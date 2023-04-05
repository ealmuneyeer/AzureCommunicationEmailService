using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
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

            EmailSendOperation emailSendOperation = await _emailClient.SendAsync(WaitUntil.Started, emailMessage);
            //EmailSendOperation emailSendOperation = _emailClient.Send(Azure.WaitUntil.Started, emailMessage);

            WriteTrace($"Email request has been sent. Attachment count: {emailMessage.Attachments.Count}; Size: {FormatSize(attachmentSize)}. Message Id: {emailSendOperation.Id}");

            //Add the send operation to the monitor list
            _messages.TryAdd(emailSendOperation, DateTime.Now);
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
                                        "==================================================");

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
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dgAttachments);

            row.Cells[ATTACHMENTS_FILE_PATH_COL_INDEX].Value = filePath;

            row.Cells[ATTACHMENTS_FILES_SIZE_COL_INDEX].Value = new FileInfo(filePath).Length.ToString("N0");

            var fileExtension = Path.GetExtension(filePath).ToLower().Substring(1);
            row.Cells[ATTACHMENTS_TYPE_COL_INDEX].Value = fileExtension;

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

            if (_emailClient == null)
            {
                if (chk429AutoRetry.Checked)
                {
                    EmailClientOptions emailClientOptions = new EmailClientOptions();
                    emailClientOptions.AddPolicy(new Catch429Policy(), HttpPipelinePosition.PerRetry);
                    _emailClient = new EmailClient(txtConnString.Text, emailClientOptions);
                }
                else
                {
                    _emailClient = new EmailClient(txtConnString.Text);
                }

                pnlInitialize.Enabled = false;
                //txtConnString.Enabled = false;
                //btnInitializeConnString.Enabled = false;
                _checkMessageStatusTimer.Start();

                WriteTrace($"429 auto retry is enabled: {chk429AutoRetry.Checked}");
                WriteTrace("Initialization succeeded");
            }

            return true;
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
                                "==================================================");
            }
        }
    }
}