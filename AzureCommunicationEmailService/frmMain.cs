using Azure;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using AzureCommunicationEmailService.EmailManagers;
using AzureCommunicationEmailService.Models;
using HeyRed.Mime;

namespace AzureCommunicationEmailService
{
    public partial class frmMain : Form
    {
        private ConfigWrapper configWrapper;

        private class ReceipentType
        {
            public const string TO = "To";
            public const string CC = "CC";
            public const string BCC = "BCC";
            public const string REPLY_TO = "Reply To";
        }

        private class ReceipentEmailColumnIndex
        {
            public const int EMAIL = 0;
            public const int DISPLAY_NAME = 1;
            public const int TYPE = 2;
        }

        private class AttachmentColumnIndex
        {
            public const int FILE_PATH = 0;
            public const int MIME_TYPE = 1;
            public const int FILES_SIZE = 2;
            public const int CONTENT_ID = 3;
            public const int IS_Inline = 4;
        }

        private class CustomHeaderColumnIndex
        {
            public const int NAME = 0;
            public const int VALUE = 1;
        }

        private class DropAuthTypeIndex
        {
            public const int ACS_KEY = 0;
            public const int AAD_DEFAULT = 1;
            public const int AAD_CLIENT = 2;
            public const int INTERACTIVE = 3;
        }


        private MessageDeliveryStatusManager _msgDeliveryStatusManager = null;
        private List<EmailClientManagerBase> _emailClientList;

        public delegate void WriteTraceDelegate(string message);
        public delegate void WriteExceptionDelegate(Exception ex);

        public frmMain()
        {
            InitializeComponent();

            configWrapper = new ConfigWrapper(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build());

            Helpers.ApplicationVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;

            this.Text += $" (v{Helpers.GetFormattedApplicationVersion()})";

            txtAcsEndpoint.Text = configWrapper.ACSEndpoint;
            txtAccessKey.Text = configWrapper.ACSAccessKey;
            txtSmtpEndpoint.Text = configWrapper.SmtpEndpoint;
            txtSmtpPort.Text = configWrapper.SmtpPort.ToString();
            txtFrom.Text = configWrapper.From;
            txtSubject.Text = configWrapper.Subject;
            chkIsHtmlBody.Checked = configWrapper.UseHtmlBody;
            txtBody.Text = configWrapper.UseHtmlBody ? configWrapper.HtmlBody : configWrapper.Body;
            FillReceipents(configWrapper.To, ReceipentType.TO);
            FillReceipents(configWrapper.CC, ReceipentType.CC);
            FillReceipents(configWrapper.BCC, ReceipentType.BCC);
            FillReceipents(configWrapper.ReplyTo, ReceipentType.REPLY_TO);

            FillAttachments(configWrapper.Attachments);

            if (configWrapper.Importance != null)
            {
                cmbImportance.Text = configWrapper.Importance.ToString();
            }

            FillCustomHeaders(configWrapper.CustomHeaders);

            FillAuthenticationType(configWrapper.AuthenticationType);

            FillCustomCredentials(configWrapper.TenantId, configWrapper.AAD_ClientId, configWrapper.AAD_ClientSecret);

            cmbSendWaitUntil.SelectedIndex = 0;

            _emailClientList = new List<EmailClientManagerBase>
            {
                new AcsSdkManager(),
                new MailKitManager(),
                new NetMailManager()
            };

            _emailClientList.ForEach((manager) =>
            {
                cmbClientType.Items.Add(manager.Name);
            });

            cmbClientType.SelectedIndex = 0;

            WriteTrace($"Application verson: {Helpers.GetFormattedApplicationVersion()}");
        }

        private void FillReceipents(List<MailAddress> emails, string type)
        {
            foreach (var email in emails)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgReceipeints);

                row.Cells[ReceipentEmailColumnIndex.EMAIL].Value = email.Address;
                row.Cells[ReceipentEmailColumnIndex.DISPLAY_NAME].Value = email.DisplayName;
                row.Cells[ReceipentEmailColumnIndex.TYPE].Value = type;
                dgReceipeints.Rows.Add(row);
            }
        }

        private bool areAttachmentsValid()
        {
            string errorMessage;

            for (int i = 0; i < dgAttachments.RowCount; i++)
            {
                string filePath = dgAttachments.Rows[i].Cells[AttachmentColumnIndex.FILE_PATH].Value.ToString();
                object? fileType = dgAttachments.Rows[i].Cells[AttachmentColumnIndex.MIME_TYPE].Value;

                if (!File.Exists(filePath))
                {
                    errorMessage = $"Attachment file '{filePath}' does not exists!";
                    WriteTrace(errorMessage);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgAttachments.Focus();
                    return false;
                }
                else if (fileType == null)
                {
                    errorMessage = $"Attachment file '{filePath}' type cannot be empty!";
                    WriteTrace(errorMessage);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgAttachments.Focus();
                    return false;
                }
            }

            return true;
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
                if (ex is AggregateException)
                {
                    ex = ((AggregateException)ex).Flatten();
                }

                txtTrace.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] Unhandled exception occured! {Environment.NewLine}" +
                  $"================================================== {Environment.NewLine}" +
                  $"{ex.Message.TrimEnd()}" +
                  $"{Environment.NewLine}=================================================={Environment.NewLine}");
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
            FillAttachment(new CustomAttachment() { FilePath = filePath });
        }

        private void FillAttachment(CustomAttachment attachment)
        {
            FileInfo fileInfo = new FileInfo(attachment.FilePath);
            DataGridViewRow row = new DataGridViewRow();

            row.CreateCells(dgAttachments);

            row.Cells[AttachmentColumnIndex.FILE_PATH].Value = attachment.FilePath;
            row.Cells[AttachmentColumnIndex.FILES_SIZE].Value = fileInfo.Length.ToString("N0");
            row.Cells[AttachmentColumnIndex.MIME_TYPE].Value = MimeTypesMap.GetMimeType(attachment.FilePath);
            row.Cells[AttachmentColumnIndex.CONTENT_ID].Value = string.IsNullOrEmpty(attachment.ContentId) ? $"{Path.GetFileNameWithoutExtension(attachment.FilePath).Replace(" ", string.Empty)}-{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}" : attachment.ContentId;
            row.Cells[AttachmentColumnIndex.IS_Inline].Value = attachment.IsInline;

            dgAttachments.Rows.Add(row);
        }


        private void FillAttachments(List<CustomAttachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                FillAttachment(attachment);
            }
        }

        private void FillCustomHeaders(List<CustomHeader> customHeaders)
        {
            foreach (var customHeader in customHeaders)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgCustomHeaders);

                row.Cells[CustomHeaderColumnIndex.NAME].Value = customHeader.Name;
                row.Cells[CustomHeaderColumnIndex.VALUE].Value = customHeader.Value;

                dgCustomHeaders.Rows.Add(row);
            }
        }

        private void FillAuthenticationType(string authType)
        {
            if (authType.Equals(AuthenticationType.ACS_KEY, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.ACS_KEY;
            }
            else if (authType.Equals(AuthenticationType.AAD_DEFAULT_CREDENTIALS, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.AAD_DEFAULT;
            }
            else if (authType.Equals(AuthenticationType.AAD_CLIENT_SECRESTS, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.AAD_CLIENT;
            }
            else if (authType.Equals(AuthenticationType.INTERACTIVE, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.INTERACTIVE;
            }
            else
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.ACS_KEY;
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
                    totalSize = totalSize + int.Parse(senderGrid.Rows[i].Cells[AttachmentColumnIndex.FILES_SIZE].Value.ToString(), System.Globalization.NumberStyles.AllowThousands);
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

                lblAttachmentsSize.Text = (int.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands) + Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[AttachmentColumnIndex.FILES_SIZE].Value.ToString(), System.Globalization.NumberStyles.AllowThousands)).ToString("N0");
            }
        }

        private void btnGetMsgDeliveryStatus_Click(object sender, EventArgs e)
        {
            InitializeEmailClientManagers();

            if (_msgDeliveryStatusManager.IsInitialized == false)
            {
                WriteTrace("Getting message delivery status failed. Message delivery status manager initialization failed");
                return;
            }
            _msgDeliveryStatusManager.CheckDeliveryStatus(txtMessageID.Text.Trim());
        }

        private void btnAADConfig_Click(object sender, EventArgs e)
        {
            AADCredentials credentials;
            Helpers.CredentialsSource source;

            if (cmbAuthType.SelectedIndex == DropAuthTypeIndex.AAD_DEFAULT)
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
            btnAADConfig.Enabled = pnlSmtpConfig.Enabled = cmbAuthType.SelectedIndex == DropAuthTypeIndex.AAD_DEFAULT || cmbAuthType.SelectedIndex == DropAuthTypeIndex.AAD_CLIENT;
        }

        private void btnShowAcsKey_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "Show")
            {
                button.Text = "Hide";
                txtAccessKey.UseSystemPasswordChar = false;
            }
            else
            {
                button.Text = "Show";
                txtAccessKey.UseSystemPasswordChar = true;
            }
        }

        private void InitializeEmailClientManagers()
        {
            if (_msgDeliveryStatusManager != null)
            {
                return;
            }

            WriteTrace("Initializing...");

            EmailClientConfiguration.AuthenticationType authenticationType;

            switch (cmbAuthType.SelectedIndex)
            {
                case DropAuthTypeIndex.ACS_KEY:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AcsKey;
                    break;

                case DropAuthTypeIndex.AAD_DEFAULT:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AADDefaultCredentials;
                    break;

                case DropAuthTypeIndex.AAD_CLIENT:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AADClientSecrets;
                    break;

                case DropAuthTypeIndex.INTERACTIVE:
                    authenticationType = EmailClientConfiguration.AuthenticationType.Interactive;
                    break;

                default:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AcsKey;
                    WriteTrace($"Authentication type is not selected. Select default: {AuthenticationType.ACS_KEY}");
                    break;
            }

            int smtpPort;
            string acsEndpoint = txtAcsEndpoint.Text.Trim();
            string acsKey = txtAccessKey.Text.Trim();
            string smtpEndpoint = txtSmtpEndpoint.Text.Trim();
            Int32.TryParse(txtSmtpPort.Text.Trim(), out smtpPort);
            bool autoRetry = chk429AutoRetry.Checked;
            AADCredentials credentials = cmbAuthType.SelectedIndex == DropAuthTypeIndex.AAD_CLIENT ? Helpers.ClientCredentials : (cmbAuthType.SelectedIndex == DropAuthTypeIndex.AAD_DEFAULT ? Helpers.EnvironmentVarCredentials : null);

            EmailClientConfiguration emailClientConfiguration = new EmailClientConfiguration(authenticationType, acsEndpoint, acsKey, smtpEndpoint, smtpPort, autoRetry, credentials);

            _msgDeliveryStatusManager = new MessageDeliveryStatusManager();
            _msgDeliveryStatusManager.Initialize(emailClientConfiguration, WriteTrace, WriteException);

            btnGetMsgDeliveryStatus.Enabled = _msgDeliveryStatusManager.IsInitialized;

            _emailClientList.ForEach((manager) =>
            {
                manager.Initialize(emailClientConfiguration, _msgDeliveryStatusManager, WriteTrace, WriteException);
            });

            pnlInitialize.Enabled = false;
        }

        private void SendEmail()
        {
            EmailClientManagerBase emailClientManager = GetEmailClientManager();

            if (emailClientManager?.IsInitialized == true)
            {
                //Fill email body
                EmailPayload emailPayload = new EmailPayload();
                emailPayload.Subject = txtSubject.Text.Trim();
                emailPayload.From = new MailAddress(txtFrom.Text.Trim(), "");
                emailPayload.Body = txtBody.Text.Trim();
                emailPayload.IsBodyHtml = chkIsHtmlBody.Checked;

                //Fill Receipent & Reply to
                for (int i = 0; i < dgReceipeints.RowCount - 1; i++)
                {
                    if (dgReceipeints.Rows[i] != null && !string.IsNullOrEmpty(dgReceipeints.Rows[i].Cells[ReceipentEmailColumnIndex.EMAIL].ToString().Trim()))
                    {
                        string emailAddress = dgReceipeints.Rows[i].Cells[ReceipentEmailColumnIndex.EMAIL].Value?.ToString();
                        string displayName = dgReceipeints.Rows[i].Cells[ReceipentEmailColumnIndex.DISPLAY_NAME].Value?.ToString();
                        string type = dgReceipeints.Rows[i].Cells[ReceipentEmailColumnIndex.TYPE].Value?.ToString() ?? "";

                        if (type.Equals(ReceipentType.CC))
                        {
                            emailPayload.CC.Add(new MailAddress(emailAddress, displayName));
                        }
                        else if (type.Equals(ReceipentType.BCC))
                        {
                            emailPayload.Bcc.Add(new MailAddress(emailAddress, displayName));
                        }
                        else if (type.Equals(ReceipentType.TO))
                        {
                            emailPayload.To.Add(new MailAddress(emailAddress, displayName));
                        }
                        else if (type.Equals(ReceipentType.REPLY_TO))
                        {
                            emailPayload.ReplyTo.Add(new MailAddress(emailAddress, displayName));
                        }
                    }
                }

                // Fill attacments
                emailPayload.AttachmentSize = long.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands);
                emailPayload.FormatedAttachmentSize = FormatSize(emailPayload.AttachmentSize);
                for (int i = 0; i < dgAttachments.RowCount; i++)
                {
                    string filePath = dgAttachments.Rows[i].Cells[AttachmentColumnIndex.FILE_PATH].Value.ToString();
                    string contentType = dgAttachments.Rows[i].Cells[AttachmentColumnIndex.MIME_TYPE].Value.ToString();
                    string contentId = dgAttachments.Rows[i].Cells[AttachmentColumnIndex.CONTENT_ID].Value.ToString();
                    bool isInline = Convert.ToBoolean(((DataGridViewCheckBoxCell)dgAttachments.Rows[i].Cells[AttachmentColumnIndex.IS_Inline]).Value?.ToString());

                    emailPayload.Attachments.Add(new CustomAttachment()
                    {
                        FilePath = filePath,
                        MIMEType = contentType,
                        ContentId = contentId,
                        IsInline = isInline
                    });
                }

                emailPayload.Priority = (short)(cmbImportance.SelectedIndex > 0 ? Convert.ToInt16(cmbImportance.Text) : 0);

                //Add custom headers
                for (int i = 0; i < dgCustomHeaders.RowCount - 1; i++)
                {
                    emailPayload.Headers.Add(new KeyValuePair<string, string>(dgCustomHeaders.Rows[i].Cells[CustomHeaderColumnIndex.NAME].Value?.ToString(), dgCustomHeaders.Rows[i].Cells[CustomHeaderColumnIndex.VALUE].Value?.ToString()));
                }

                emailPayload.WaitUntil = cmbSendWaitUntil.Text.Equals("Completed", StringComparison.InvariantCultureIgnoreCase) ? WaitUntil.Completed : WaitUntil.Started;
                emailPayload.CountOfEmails = Convert.ToInt32(numEmailsToSend.Value);

                emailClientManager.SendEmail(emailPayload);
            }
            else
            {
                WriteTrace($"Sending email failed because selected client initialization failed");
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            InitializeEmailClientManagers();

            SendEmail();
        }

        private EmailClientManagerBase GetEmailClientManager()
        {
            return _emailClientList.FirstOrDefault((client) => client.Name == cmbClientType.SelectedItem.ToString());
        }

        private void btnStopSendingEmail_Click(object sender, EventArgs e)
        {
            EmailClientManagerBase emailClientManager = GetEmailClientManager();

            if (emailClientManager?.IsInitialized != null)
            {
                emailClientManager.StopSendingEmails();
            }
        }
    }
}