using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Timers;

namespace AzureCommunicationEmailService
{
    public partial class frmMain : Form
    {

        private ConfigWrapper configWrapper;

        private const int RECEIPEINT_EMAIL_COL_INDEX = 0;
        private const int RECEIPEINT_DISPLAY_NAME_COL_INDEX = 1;
        private const int RECEIPEINT_TYPE_COL_INDEX = 2;

        private const string RECEIPEINT_TYPE_TO = "To";
        private const string RECEIPEINT_TYPE_CC = "CC";
        private const string RECEIPEINT_TYPE_BCC = "BCC";
        private const string RECEIPEINT_TYPE_REPLY_TO = "Reply To";

        private const int ATTACHMENTS_FILE_PATH_COL_INDEX = 0;
        private const int ATTACHMENTS_TYPE_COL_INDEX = 1;
        private const int ATTACHMENTS_FILES_SIZE_COL_INDEX = 2;

        private const int CUSTOM_HEADER_NAME_COL_INDEX = 0;
        private const int CUSTOM_HEADER_VALUE_COL_INDEX = 1;

        private const int DRP_AUTH_TYPE_ACS_KEY_INDEX = 0;
        private const int DRP_AUTH_TYPE_AAD_DEFAULT_INDEX = 1;
        private const int DRP_AUTH_TYPE_AAD_CLIENT_INDEX = 2;
        private const int DRP_AUTH_TYPE_INTERACTIVE_INDEX = 3;

        private System.Timers.Timer _checkMessageStatusTimer;
        private ConcurrentDictionary<EmailSendOperation, DateTime> _messages = new ConcurrentDictionary<EmailSendOperation, DateTime>(); //Key: EmailSendOperation, value: when to check for update
        private EmailClient _emailClient = null;


        public frmMain()
        {
            InitializeComponent();

            configWrapper = new ConfigWrapper(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build());

            txtConnString.Text = configWrapper.ConnectionString;
            txtFrom.Text = configWrapper.From;
            txtSubject.Text = configWrapper.Subject;
            chkIsHtmlBody.Checked = configWrapper.UseHtmlBody;
            txtBody.Text = configWrapper.UseHtmlBody ? configWrapper.HtmlBody : configWrapper.Body;
            FillReceipents(configWrapper.To, RECEIPEINT_TYPE_TO);
            FillReceipents(configWrapper.CC, RECEIPEINT_TYPE_CC);
            FillReceipents(configWrapper.BCC, RECEIPEINT_TYPE_BCC);
            FillReceipents(configWrapper.ReplyTo, RECEIPEINT_TYPE_REPLY_TO);

            FillAttachments(configWrapper.Attachments);

            if (configWrapper.Importance != null)
            {
                cmbImportance.Text = configWrapper.Importance.ToString();
            }

            FillCustomHeaders(configWrapper.CustomHeaders);

            FillAuthenticationType(configWrapper.AuthenticationType);

            FillCustomCredentials(configWrapper.TenantId, configWrapper.AAD_ClientId, configWrapper.AAD_ClientSecret);

            cmbSendWaitUntil.SelectedIndex = 0;

            _checkMessageStatusTimer = new System.Timers.Timer(1000);
            _checkMessageStatusTimer.AutoReset = false;
            _checkMessageStatusTimer.Elapsed += CheckMessageStatusTimer_Elapsed;
        }

        private void FillReceipents(List<ConfigEmailAddress> emails, string type)
        {
            foreach (var email in emails)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgReceipeints);

                row.Cells[RECEIPEINT_EMAIL_COL_INDEX].Value = email.Email;
                row.Cells[RECEIPEINT_DISPLAY_NAME_COL_INDEX].Value = email.DisplayName;
                row.Cells[RECEIPEINT_TYPE_COL_INDEX].Value = type;
                dgReceipeints.Rows.Add(row);
            }
        }

        private bool IsInputValid(out string errorMessage)
        {
            errorMessage = "";

            //Validate email client
            if (_emailClient == null && InitializeEmailClient() == false)
            {
                MessageBox.Show("Please fill ACS connection string and select Initialize button", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnString.Focus();
                errorMessage = "Email client is not initialized";
                return false;
            }

            //Validate attachment file exists and type selected
            for (int i = 0; i < dgAttachments.RowCount; i++)
            {
                string filePath = dgAttachments.Rows[i].Cells[ATTACHMENTS_FILE_PATH_COL_INDEX].Value.ToString();
                object? fileType = dgAttachments.Rows[i].Cells[ATTACHMENTS_TYPE_COL_INDEX].Value;

                if (!File.Exists(filePath))
                {
                    errorMessage = $"Attachment file '{filePath}' does not exists!";
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgAttachments.Focus();
                    return false;
                }
                else if (fileType == null)
                {
                    errorMessage = $"Attachment file '{filePath}' type cannot be empty!";
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgAttachments.Focus();
                    return false;
                }
            }

            return true;
        }

        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            WriteTrace("Start sending email...");

            string validationErrorMessage;

            if (IsInputValid(out validationErrorMessage) == false)
            {
                WriteTrace("Sending fialed. " + validationErrorMessage);
                return;
            }

            //Fill email body
            EmailContent emailContent = new EmailContent(txtSubject.Text);
            if (chkIsHtmlBody.Checked)
            {
                emailContent.Html = txtBody.Text + $"<br/><hr/><div>Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC</div>";
            }
            else
            {
                emailContent.PlainText = txtBody.Text + Environment.NewLine + $"Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC";
            }

            //Fill Receipent & Reply to
            List<EmailAddress> receipeintToEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintCCEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintBCCEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintReplyToEmailAddresses = new List<EmailAddress>();
            for (int i = 0; i < dgReceipeints.RowCount - 1; i++)
            {
                if (dgReceipeints.Rows[i] != null && !string.IsNullOrEmpty(dgReceipeints.Rows[i].Cells[RECEIPEINT_EMAIL_COL_INDEX].ToString().Trim()))
                {
                    string emailAddress = dgReceipeints.Rows[i].Cells[RECEIPEINT_EMAIL_COL_INDEX].Value?.ToString();
                    string displayName = dgReceipeints.Rows[i].Cells[RECEIPEINT_DISPLAY_NAME_COL_INDEX].Value?.ToString();

                    if (dgReceipeints.Rows[i].Cells[RECEIPEINT_TYPE_COL_INDEX].Value.Equals(RECEIPEINT_TYPE_CC))
                    {
                        receipeintCCEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[RECEIPEINT_TYPE_COL_INDEX].Value.Equals(RECEIPEINT_TYPE_BCC))
                    {
                        receipeintBCCEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[RECEIPEINT_TYPE_COL_INDEX].Value.Equals(RECEIPEINT_TYPE_TO))
                    {
                        receipeintToEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[RECEIPEINT_TYPE_COL_INDEX].Value.Equals(RECEIPEINT_TYPE_REPLY_TO))
                    {
                        receipeintReplyToEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                }
            }

            EmailRecipients emailRecipients = new EmailRecipients(receipeintToEmailAddresses, receipeintCCEmailAddresses, receipeintBCCEmailAddresses);

            EmailMessage emailMessage = new EmailMessage(txtFrom.Text, emailRecipients, emailContent);

            receipeintReplyToEmailAddresses.ForEach(r => emailMessage.ReplyTo.Add(r));


            // Fill attacments
            long attachmentSize = long.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands);
            for (int i = 0; i < dgAttachments.RowCount; i++)
            {
                string filePath = dgAttachments.Rows[i].Cells[ATTACHMENTS_FILE_PATH_COL_INDEX].Value.ToString();
                string attachmentName = Path.GetFileName(filePath);
                string contentType = dgAttachments.Rows[i].Cells[ATTACHMENTS_TYPE_COL_INDEX].Value.ToString();

                byte[] bytes = File.ReadAllBytes(filePath);
                BinaryData attachmentBinaryData = new BinaryData(bytes);

                EmailAttachment emailAttachment = new EmailAttachment(attachmentName, contentType, attachmentBinaryData);

                emailMessage.Attachments.Add(emailAttachment);
            }

            //Set email importance
            if (cmbImportance.SelectedIndex > 0)
            {
                emailMessage.Headers.Add("x-priority", cmbImportance.Text);
            }

            //Add custom headers
            for (int i = 0; i < dgCustomHeaders.RowCount - 1; i++)
            {
                emailMessage.Headers.Add(dgCustomHeaders.Rows[i].Cells[CUSTOM_HEADER_NAME_COL_INDEX].Value?.ToString(), dgCustomHeaders.Rows[i].Cells[CUSTOM_HEADER_VALUE_COL_INDEX].Value?.ToString());
            }

            WaitUntil waitUntil = cmbSendWaitUntil.Text.Equals("Completed", StringComparison.InvariantCultureIgnoreCase) ? WaitUntil.Completed : WaitUntil.Started;

            //Send multiple emails
            Stopwatch allEmailsStopwatch = new Stopwatch();
            Stopwatch singleEmailstopwatch = new Stopwatch();
            EmailSendOperation emailSendOperation;

            allEmailsStopwatch.Start();
            for (int i = 0; i < numEmailsToSend.Value; i++)
            {
                singleEmailstopwatch.Restart();

                emailSendOperation = await _emailClient.SendAsync(waitUntil, emailMessage);

                singleEmailstopwatch.Stop();

                WriteTrace($"Email request #{i + 1} has been sent. Waited until: {waitUntil}; Time elapsed: {singleEmailstopwatch.Elapsed.ToString("mm\\:ss\\.fff")}; Attachment count: {emailMessage.Attachments.Count}; Size: {FormatSize(attachmentSize)}. Message Id: {emailSendOperation?.Id}");

                //Add the send operation to the monitor list
                _messages.TryAdd(emailSendOperation, DateTime.Now);
            }

            allEmailsStopwatch.Stop();
            WriteTrace($"***** {numEmailsToSend.Value} email(s) has been sent. Waited until: {waitUntil}; Time elapsed: {allEmailsStopwatch.Elapsed.ToString("mm\\:ss\\.fff")}");
        }

        private void WriteTrace(string message)
        {
            if (txtTrace.InvokeRequired)
            {
                txtTrace.Invoke(new Action(() =>
                {
                    WriteTrace(message);
                }));
            }
            else
            {
                txtTrace.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}{Environment.NewLine}");
            }
        }

        public void WriteException(Exception ex)
        {
            if (txtTrace.InvokeRequired)
            {
                txtTrace.Invoke(new Action(() =>
                {
                    WriteException(ex);
                }));
            }
            else
            {
                txtTrace.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Unhandled exception occured! {Environment.NewLine}" +
                  $"================================================== {Environment.NewLine}" +
                  $"{ex.Message.TrimEnd()}" +
                  $"{Environment.NewLine}=================================================={Environment.NewLine}");
            }
        }

        private async void CheckMessageStatusTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_messages.Count == 0)
                {
                    return;
                }
                else if (_emailClient == null && InitializeEmailClient() == false)
                {
                    WriteTrace($"Failed to initialize email client. Exit checking send status for message Id(s) {String.Join(", ", _messages.Select(m => m.Key.Id).ToArray())}");
                    _messages.Clear();
                    return;
                }

                foreach (var message in _messages)
                {
                    try
                    {
                        await message.Key.UpdateStatusAsync();

                        WriteTrace($"Message {message.Key.Id}; status: {(message.Key.HasValue ? message.Key.Value.Status : "n/a")}");

                        if (message.Key.HasCompleted)
                        {
                            DateTime tempTime;
                            _messages.Remove(message.Key, out tempTime);
                            break;
                        }
                    }
                    catch (RequestFailedException ex)
                    {
                        WriteTrace($"Exception occureed while retrieving Message {message.Key.Id} delivery status with error code {ex.ErrorCode} {Environment.NewLine}" +
                                        $"================================================== {Environment.NewLine}" +
                                        $"{ex.Message}" +
                                        $"{Environment.NewLine}==================================================");

                        WriteTrace($"Removing message {message.Key.Id} from monitoring queue");
                        DateTime tempTime;
                        _messages.Remove(message.Key, out tempTime);
                    }
                }
            }
            finally
            {
                _checkMessageStatusTimer.Start();
            }
        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.Multiselect = false;
            fileDialog.CheckPathExists = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                FillAttachment(fileDialog.FileName);
            }
        }

        private void FillAttachment(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            DataGridViewRow row = new DataGridViewRow();

            row.CreateCells(dgAttachments);

            row.Cells[ATTACHMENTS_FILE_PATH_COL_INDEX].Value = filePath;
            row.Cells[ATTACHMENTS_FILES_SIZE_COL_INDEX].Value = fileInfo.Length.ToString("N0");
            row.Cells[ATTACHMENTS_TYPE_COL_INDEX].Value = fileInfo.Extension.TrimStart('.');

            dgAttachments.Rows.Add(row);
        }

        private void FillAttachments(List<string> filesPath)
        {
            foreach (string filePath in filesPath)
            {
                FillAttachment(filePath);
            }
        }

        private void FillCustomHeaders(List<CustomHeader> customHeaders)
        {
            foreach (var customHeader in customHeaders)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgCustomHeaders);

                row.Cells[CUSTOM_HEADER_NAME_COL_INDEX].Value = customHeader.Name;
                row.Cells[CUSTOM_HEADER_VALUE_COL_INDEX].Value = customHeader.Value;

                dgCustomHeaders.Rows.Add(row);
            }
        }

        private void FillAuthenticationType(string authType)
        {
            if (authType.Equals(Helpers.AuthenticationType.ACS_KEY, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DRP_AUTH_TYPE_ACS_KEY_INDEX;
            }
            else if (authType.Equals(Helpers.AuthenticationType.AAD_DEFAULT_CREDENTIALS, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DRP_AUTH_TYPE_AAD_DEFAULT_INDEX;
            }
            else if (authType.Equals(Helpers.AuthenticationType.AAD_CLIENT_SECRESTS, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DRP_AUTH_TYPE_AAD_CLIENT_INDEX;
            }
            else if (authType.Equals(Helpers.AuthenticationType.AAD_INTERACTIVE, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DRP_AUTH_TYPE_INTERACTIVE_INDEX;
            }
            else
            {
                cmbAuthType.SelectedIndex = DRP_AUTH_TYPE_ACS_KEY_INDEX;
                WriteTrace($"Unexpected/Empty authentication type '{authType}'. Default authentication 'AcsKey' will be used");
            }
        }

        private void FillCustomCredentials(string tenantId, string clientId, string clientSecret)
        {
            Helpers.UpdateClientCredentials(tenantId, clientId, clientSecret);
        }

        private void dgAttachments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                senderGrid.Rows.RemoveAt(e.RowIndex);
            }
        }

        private string FormatSize(long sizeBytes)
        {
            decimal decimalSize = Convert.ToDecimal(sizeBytes);

            if (sizeBytes < 1000)
            {
                return sizeBytes.ToString("N") + "Byte";
            }
            else if (sizeBytes < 1000000)
            {
                return (decimalSize / 1000).ToString("N") + "KB";
            }
            else if (sizeBytes < 1000000000)
            {
                return (decimalSize / 1000000).ToString("N") + "MB";
            }
            else
            {
                return (decimalSize / 1000000000).ToString("N") + "GB";
            }
        }

        private void dgAttachments_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowCount > 0)
            {
                lblAttachmentsCount.Text = senderGrid.RowCount.ToString();

                int totalSize = 0;
                for (int i = 0; i < senderGrid.RowCount; i++)
                {
                    totalSize = totalSize + int.Parse(senderGrid.Rows[i].Cells[ATTACHMENTS_FILES_SIZE_COL_INDEX].Value.ToString(), System.Globalization.NumberStyles.AllowThousands);
                }

                lblAttachmentsSize.Text = totalSize.ToString("N0");
            }
        }

        private void dgAttachments_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowCount > 0)
            {
                lblAttachmentsCount.Text = (Convert.ToInt32(lblAttachmentsCount.Text) + 1).ToString();

                lblAttachmentsSize.Text = (int.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands) + Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[ATTACHMENTS_FILES_SIZE_COL_INDEX].Value.ToString(), System.Globalization.NumberStyles.AllowThousands)).ToString("N0");
            }
        }

        private void btnInitializeConnString_Click(object sender, EventArgs e)
        {
            InitializeEmailClient();
        }

        private bool InitializeEmailClient()
        {
            WriteTrace("Initializing email client...");

            if (string.IsNullOrEmpty(txtConnString.Text.Trim()))
            {
                WriteTrace("Initialization failed. Please fill connection string");

                MessageBox.Show("Please fill connection string", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConnString.Focus();
                return false;
            }
            else if (cmbAuthType.SelectedIndex == -1)
            {
                WriteTrace("Initialization failed. Please select authentication method");

                MessageBox.Show("Please select authentication method", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAuthType.Focus();
                return false;
            }

            if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_DEFAULT_INDEX)
            {
                string missingConfigErrorMsg = "Initialization will continue with wanring. Environment varilbale {0} is missing or empty!";

                if (string.IsNullOrEmpty(Helpers.EnvironmentVarCredentials.TenantId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_TENANT_ID));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_TENANT_ID), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(Helpers.EnvironmentVarCredentials.ClientId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_CLIENT_ID));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_CLIENT_ID), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(Helpers.EnvironmentVarCredentials.ClientSecret.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_CLIENT_SECRET));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, Helpers.EnvironmentVariable.AZURE_CLIENT_SECRET), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_CLIENT_INDEX)
            {
                string missingConfigErrorMsg = "Initialization will continue with wanring. {0} is empty!";

                if (string.IsNullOrEmpty(configWrapper.TenantId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "Tenant Id"));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, "Tenant Id"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(configWrapper.AAD_ClientId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "AAD_ClientId"));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, "AAD_ClientId"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrEmpty(configWrapper.AAD_ClientSecret.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "AAD_ClientSecret"));

                    MessageBox.Show(string.Format(missingConfigErrorMsg, "AAD_ClientSecret"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (_emailClient == null)
            {
                EmailClientOptions emailClientOptions = null;

                if (chk429AutoRetry.Checked == false)
                {
                    emailClientOptions = new EmailClientOptions();
                    emailClientOptions.AddPolicy(new Catch429Policy(), HttpPipelinePosition.PerRetry);
                }

                if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_ACS_KEY_INDEX)
                {
                    _emailClient = new EmailClient(txtConnString.Text, emailClientOptions);
                }
                else if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_DEFAULT_INDEX)
                {
                    Uri endPoint = new Uri(GetEndpoint(txtConnString.Text));
                    _emailClient = new EmailClient(endPoint, new DefaultAzureCredential());
                }
                else if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_CLIENT_INDEX)
                {
                    ClientSecretCredential clientSecretCredential = new ClientSecretCredential(Helpers.ClientCredentials.TenantId, Helpers.ClientCredentials.ClientId, Helpers.ClientCredentials.ClientSecret);
                    Uri endPoint = new Uri(GetEndpoint(txtConnString.Text));
                    _emailClient = new EmailClient(endPoint, clientSecretCredential);
                }
                else if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_INTERACTIVE_INDEX)
                {
                    Uri endPoint = new Uri(GetEndpoint(txtConnString.Text));
                    _emailClient = new EmailClient(endPoint, new InteractiveBrowserCredential());

                }

                pnlInitialize.Enabled = false;
                _checkMessageStatusTimer.Start();

                WriteTrace($"429 auto retry is enabled: {chk429AutoRetry.Checked}");
                WriteTrace($"Authentication type: {cmbAuthType.Text}");
                WriteTrace("Initialization succeeded");
            }

            return true;
        }

        public string GetEndpoint(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString.Trim()))
            {
                return "";
            }

            return connectionString.Substring(connectionString.IndexOf('=') + 1, connectionString.IndexOf(';') - ("endpoint=".Count()));
        }

        private async void btnGetMsgDeliveryStatus_Click(object sender, EventArgs e)
        {
            string tempMsgId = txtMessageID.Text.Trim();

            WriteTrace($"Getting email delivery status for message {tempMsgId}...");

            //Validate email client
            if (_emailClient == null && InitializeEmailClient() == false)
            {
                MessageBox.Show("Please fill ACS connection string and select Initialize button", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnString.Focus();
                WriteTrace("Getting email delivery status failed. Email client is not initialized");
                return;
            }

            if (string.IsNullOrEmpty(tempMsgId))
            {
                WriteTrace("Getting email delivery status failed. Fill the Message ID to get delivery status");

                MessageBox.Show("Message ID is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMessageID.Focus();
                return;
            }

            EmailSendOperation emailSendOperation = null;

            try
            {
                emailSendOperation = new EmailSendOperation(tempMsgId, _emailClient);

                await emailSendOperation.UpdateStatusAsync();

                WriteTrace($"Message {emailSendOperation.Id}; status: {(emailSendOperation.HasValue ? emailSendOperation.Value.Status : "n/a")}");
            }
            catch (RequestFailedException ex)
            {
                WriteTrace($"Exception occureed while retrieving Message {emailSendOperation.Id} delivery status with error code {ex.ErrorCode} {Environment.NewLine}" +
                                $"================================================== {Environment.NewLine}" +
                                $"{ex.Message}" +
                                $"{Environment.NewLine}==================================================");
            }
        }

        private void btnAADConfig_Click(object sender, EventArgs e)
        {
            Helpers.AADCredentials credentials;
            Helpers.CredentialsSource source;

            if (cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_DEFAULT_INDEX)
            {
                credentials = Helpers.EnvironmentVarCredentials;
                source = Helpers.CredentialsSource.EnvironmentVariables;
            }
            else
            {
                credentials = Helpers.ClientCredentials;
                source = Helpers.CredentialsSource.AppSettings;
            }

            frmAADAuthentication frmAADAuthentication = new frmAADAuthentication(source, credentials);
            frmAADAuthentication.ShowDialog();
        }

        private void cmbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAADConfig.Enabled = cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_DEFAULT_INDEX || cmbAuthType.SelectedIndex == DRP_AUTH_TYPE_AAD_CLIENT_INDEX;
        }
    }
}