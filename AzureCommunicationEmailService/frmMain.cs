using Azure;
using AzureCommunicationEmailService.EmailManagers;
using AzureCommunicationEmailService.Models;
using HeyRed.Mime;
using Microsoft.Extensions.Configuration;
using ServiceBusReceiver;
using System.Net.Mail;

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
            public const int ENTRA_ID_DEFAULT = 1;
            public const int ENTRA_ID_CLIENT = 2;
            public const int INTERACTIVE = 3;
            public const int SMTP_BASIC_AUTH = 4;
        }

        private class dgEmailClientConfigColumnIndex
        {
            public const int NAME = 0;
            public const int VALUE = 1;
            public const int NOTES = 2;
        }

        private MessageDeliveryStatusManager _msgDeliveryStatusManager = null;
        private List<EmailClientManagerBase> _emailClientList;

        public delegate void WriteTraceDelegate(string message);
        public delegate void WriteExceptionDelegate(Exception ex);

        private ServiceBusReceiverManager _serviceBusReceiverManager = null;

        private List<EmailAuthConfig> _emailAuthConfig;

        private class EmailClientConfigName
        {
            public const string ACS_ENDPOINT = "ACS Endpoint";
            public const string ACS_ACCESS_KEY = "ACS Access Key";
            public const string TENANT_ID = "Tenant Id";
            public const string ENTRA_ID_CLIENT_ID = "Entra ID Client Id";
            public const string ENTRA_ID_CLIENT_SECRET = "Entra ID Client Secret";
            public const string ENV_VARIABLE_TENANT_ID = "Env. Variable Tenant Id";
            public const string ENV_VARIABLE_ENTRA_ID_CLIENT_ID = "Env. Variable Entra ID Client Id";
            public const string ENV_VARIABLE_ENTRA_ID_CLIENT_SECRET = "Env. Variable Entra ID Client Secret";
            public const string SMTP_ENDPOINT = "Smtp Endpoint";
            public const string SMTP_PORT = "Smtp Port";
            public const string SMTP_USERNAME = "Smtp Username";
            public const string SMTP_PASSWORD = "Smtp Password";
            public const string USE_OAUTH2 = "Use OAuth2";
            public const string AUTO_RETRY_429 = "429 Auto Retry";
        }

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

            txtFrom.Text = configWrapper.From;
            txtSubject.Text = configWrapper.Subject;
            chkIsHtmlBody.Checked = configWrapper.UseHtmlBody;
            txtBody.Text = configWrapper.UseHtmlBody ? configWrapper.HtmlBody : configWrapper.Body;
            txtServiceBusConnectionString.Text = configWrapper.ServiceBusConnectionString;
            txtServiceBusQueueName.Text = configWrapper.ServiceBusQueueName;
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

            FillCustomCredentials(configWrapper.TenantId, configWrapper.ENTRA_ID_ClientId, configWrapper.ENTRA_ID_ClientSecret);

            FillEmailAuthConfiguration();

            FillAuthenticationType(configWrapper.AuthenticationType);

            cmbSendWaitUntil.SelectedIndex = 0;

            InitializeEmailClientList();

            cmbClientType.SelectedIndex = 0;

            DisableEnableEmailOperationsTab(false);

            WriteTrace($"Application verson: {Helpers.GetFormattedApplicationVersion()}");
        }

        private void FillEmailAuthConfiguration()
        {
            _emailAuthConfig = new List<EmailAuthConfig>()
            {
                //Strings
                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ACS_ENDPOINT,
                    Value = configWrapper.ACSEndpoint,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> {DropAuthTypeIndex.ACS_KEY, DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT, DropAuthTypeIndex.INTERACTIVE, DropAuthTypeIndex.SMTP_BASIC_AUTH},
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ACS_ACCESS_KEY,
                    Value = configWrapper.ACSAccessKey,
                    IsReadOnly = false,
                    IsPassword = true,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ACS_KEY }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.TENANT_ID,
                    Value = configWrapper.TenantId,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_CLIENT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ENTRA_ID_CLIENT_ID,
                    Value = configWrapper.ENTRA_ID_ClientId,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_CLIENT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ENTRA_ID_CLIENT_SECRET,
                    Value = configWrapper.ENTRA_ID_ClientSecret,
                    IsReadOnly = false,
                    IsPassword = true,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_CLIENT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ENV_VARIABLE_TENANT_ID,
                    Value = Helpers.EnvironmentVarCredentials.TenantId,
                    IsReadOnly = true,
                    IsPassword = false,
                    Notes = "Value read from AZURE_TENANT_ID environment variable",
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_DEFAULT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ENV_VARIABLE_ENTRA_ID_CLIENT_ID,
                    Value = Helpers.EnvironmentVarCredentials.ClientId,
                    IsReadOnly = true,
                    IsPassword = false,
                    Notes = "Value read from AZURE_CLIENT_ID environment variable",
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_DEFAULT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.ENV_VARIABLE_ENTRA_ID_CLIENT_SECRET,
                    Value = Helpers.EnvironmentVarCredentials.ClientSecret,
                    IsReadOnly = true,
                    IsPassword = true,
                    Notes = "Value read from AZURE_CLIENT_SECRET environment variable",
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_DEFAULT }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.SMTP_ENDPOINT,
                    Value = configWrapper.SmtpEndpoint,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT, DropAuthTypeIndex.SMTP_BASIC_AUTH }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.SMTP_USERNAME,
                    Value = configWrapper.SmtpUsername,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> { DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT, DropAuthTypeIndex.SMTP_BASIC_AUTH }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.String)
                {
                    Name = EmailClientConfigName.SMTP_PASSWORD,
                    Value = configWrapper.SmtpPassword,
                    IsReadOnly = false,
                    IsPassword = true,
                    AuthTypes = new List<int> { DropAuthTypeIndex.SMTP_BASIC_AUTH }
                },

                //Integers
                new EmailAuthConfig(EmailAuthConfig.ValueType.Integer)
                {
                    Name = EmailClientConfigName.SMTP_PORT,
                    Value = configWrapper.SmtpPort,
                    IsReadOnly = false,
                    IsPassword = false,
                    AuthTypes = new List<int> {DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT, DropAuthTypeIndex.SMTP_BASIC_AUTH }
                },

                //Booleans
                new EmailAuthConfig(EmailAuthConfig.ValueType.Boolean)
                {
                    Name =EmailClientConfigName.AUTO_RETRY_429,
                    Value = configWrapper.AutoRetryOn429,
                    IsReadOnly = false,
                    IsPassword = false,
                    Notes = "Only available with SDK Client",
                    AuthTypes = new List<int> {DropAuthTypeIndex.ACS_KEY, DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT, DropAuthTypeIndex.INTERACTIVE }
                },

                new EmailAuthConfig(EmailAuthConfig.ValueType.Boolean)
                {
                    Name = EmailClientConfigName.USE_OAUTH2,
                    Value = configWrapper.UseOAuth2,
                    IsReadOnly = false,
                    IsPassword = false,
                    Notes="Only available with MailKit Client and Smtp Username must be provided",
                    AuthTypes = new List<int> {DropAuthTypeIndex.ENTRA_ID_DEFAULT, DropAuthTypeIndex.ENTRA_ID_CLIENT }
                },
            };
        }

        private void DisplayEmailAuthConfiguration(int authtype)
        {
            dgEmailClientConfig.Rows.Clear();

            foreach (var cell in _emailAuthConfig.Where(c => c.AuthTypes.Contains(authtype)))
            {
                DataGridViewRow newRow = new DataGridViewRow();

                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = cell.Name });

                if (cell.ValType == EmailAuthConfig.ValueType.Boolean)
                {
                    newRow.Cells.Add(new DataGridViewCheckBoxCell { Value = cell.Value });
                }
                else
                {
                    newRow.Cells.Add(new DataGridViewTextBoxCell { Value = cell.IsPassword ? cell.GetMaskedValue() : cell.Value });
                }

                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = cell.Notes });

                dgEmailClientConfig.Rows.Add(newRow);
            }
        }

        private void DisableEnableEmailOperationsTab(bool enable)
        {
            tabControl1.TabPages[1].Enabled = enable;
            grpMessageDeliveryStatus.Enabled = enable;
        }

        private void InitializeEmailClientList()
        {
            _emailClientList = new List<EmailClientManagerBase>
            {
                new AcsSdkManager(),
                new MailKitManager(),
                new NetMailManager()
            };

            cmbClientType.Items.Clear();

            _emailClientList.ForEach((manager) =>
            {
                cmbClientType.Items.Add(manager.Name);
            });

            cmbClientType.SelectedIndex = 0;
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
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.ENTRA_ID_DEFAULT;
            }
            else if (authType.Equals(AuthenticationType.AAD_CLIENT_SECRESTS, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.ENTRA_ID_CLIENT;
            }
            else if (authType.Equals(AuthenticationType.INTERACTIVE, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.INTERACTIVE;
            }
            else if (authType.Equals(AuthenticationType.SMTP_USERNAME_PASSWORD, StringComparison.InvariantCultureIgnoreCase))
            {
                cmbAuthType.SelectedIndex = DropAuthTypeIndex.SMTP_BASIC_AUTH;
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
            if (_msgDeliveryStatusManager == null || _msgDeliveryStatusManager.IsInitialized == false)
            {
                WriteTrace("Getting message delivery status failed. Message delivery status manager initialization failed");
                return;
            }
            _msgDeliveryStatusManager.CheckDeliveryStatus(txtMessageID.Text.Trim());
        }

        private void cmbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayEmailAuthConfiguration(cmbAuthType.SelectedIndex);
        }

        private void HideShowPassword(Button button, TextBox passwordTextBox)
        {
            if (button.Text == "Show")
            {
                button.Text = "Hide";
                passwordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                button.Text = "Show";
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }


        private void InitializeEmailClientManagers()
        {
            //if (_msgDeliveryStatusManager != null)
            //{
            //    return;
            //}

            WriteTrace("Initializing...");

            EmailClientConfiguration.AuthenticationType authenticationType;

            switch (cmbAuthType.SelectedIndex)
            {
                case DropAuthTypeIndex.ACS_KEY:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AcsKey;
                    break;

                case DropAuthTypeIndex.ENTRA_ID_DEFAULT:
                    authenticationType = EmailClientConfiguration.AuthenticationType.EntraIdDefaultCredentials;
                    break;

                case DropAuthTypeIndex.ENTRA_ID_CLIENT:
                    authenticationType = EmailClientConfiguration.AuthenticationType.EntraIdClientSecrets;
                    Helpers.UpdateClientCredentials(_emailAuthConfig.First(c => c.Name == EmailClientConfigName.TENANT_ID).GetStringValue().Trim(), _emailAuthConfig.First(c => c.Name == EmailClientConfigName.ENTRA_ID_CLIENT_ID).GetStringValue().Trim(), _emailAuthConfig.First(c => c.Name == EmailClientConfigName.ENTRA_ID_CLIENT_SECRET).GetStringValue().Trim());
                    break;

                case DropAuthTypeIndex.INTERACTIVE:
                    authenticationType = EmailClientConfiguration.AuthenticationType.Interactive;
                    break;

                case DropAuthTypeIndex.SMTP_BASIC_AUTH:
                    authenticationType = EmailClientConfiguration.AuthenticationType.SmtpUsernamePassword;
                    break;

                default:
                    authenticationType = EmailClientConfiguration.AuthenticationType.AcsKey;
                    WriteTrace($"Authentication type is not selected. Select default: {AuthenticationType.ACS_KEY}");
                    break;
            }

            string acsEndpoint = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.ACS_ENDPOINT).GetStringValue().Trim();
            string acsKey = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.ACS_ACCESS_KEY).GetStringValue().Trim();
            string smtpEndpoint = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.SMTP_ENDPOINT).GetStringValue().Trim();
            int smtpPort = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.SMTP_PORT).GetIntegerValue() ?? 0;
            bool autoRetry = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.AUTO_RETRY_429).GetBooleanValue() ?? false;
            bool mailKitUseOAuth2 = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.USE_OAUTH2).GetBooleanValue() ?? false;
            EntraIdCredentials entraIdCredentials = authenticationType == EmailClientConfiguration.AuthenticationType.EntraIdClientSecrets ? Helpers.ClientCredentials : (authenticationType == EmailClientConfiguration.AuthenticationType.EntraIdDefaultCredentials ? Helpers.EnvironmentVarCredentials : null);
            SmtpUsernamePassword smtpUsernamePassword = new SmtpUsernamePassword() { Username = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.SMTP_USERNAME).GetStringValue().Trim(), Password = _emailAuthConfig.First(c => c.Name == EmailClientConfigName.SMTP_PASSWORD).GetStringValue().Trim() };

            EmailClientConfiguration emailClientConfiguration = new EmailClientConfiguration(authenticationType, acsEndpoint, acsKey, smtpEndpoint, smtpPort, autoRetry, entraIdCredentials, smtpUsernamePassword, mailKitUseOAuth2);

            _msgDeliveryStatusManager = new MessageDeliveryStatusManager();
            _msgDeliveryStatusManager.Initialize(emailClientConfiguration, WriteTrace, WriteException);

            _emailClientList.ForEach((manager) =>
            {
                manager.Initialize(emailClientConfiguration, _msgDeliveryStatusManager, WriteTrace, WriteException);
            });

            pnlInitializeEmailClients.Enabled = btnInitializeEmailClients.Enabled = false;

            DisableEnableEmailOperationsTab(true);
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

        private void btnGetBase64String_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.Multiselect = false;
            fileDialog.CheckPathExists = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(fileDialog.FileName);
                string base64ImageRepresentation = Convert.ToBase64String(imageBytes);

                Clipboard.SetText($"data:{MimeTypesMap.GetMimeType(fileDialog.FileName)};base64,{base64ImageRepresentation}");

                MessageBox.Show("The Base64 string has been successfully copied to the clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            InitializeEmailClientManagers();
        }

        private void btnReinitialize_Click(object sender, EventArgs e)
        {
            WriteTrace("Reset email clients...");

            if (_msgDeliveryStatusManager != null)
            {
                _msgDeliveryStatusManager.Dispose();
                _msgDeliveryStatusManager = null;
            }

            _emailClientList.Clear();

            InitializeEmailClientList();

            pnlInitializeEmailClients.Enabled = btnInitializeEmailClients.Enabled = true;

            DisableEnableEmailOperationsTab(false);

            WriteTrace("Reset email clients succeeded");
        }

        private void btnShowServiceBusConnString_Click(object sender, EventArgs e)
        {
            HideShowPassword((Button)sender, txtServiceBusConnectionString);
        }

        private void btnServiceBusStartMonitoring_Click(object sender, EventArgs e)
        {
            WriteTrace("Start monitoring email events...");
            if (_serviceBusReceiverManager == null)
            {
                _serviceBusReceiverManager = new ServiceBusReceiverManager(txtServiceBusConnectionString.Text, txtServiceBusQueueName.Text);
            }

            _serviceBusReceiverManager.ServiceBusEvent += ServiceBusEventHandler;
            _serviceBusReceiverManager.Start();

            pnlServiceBusConfig.Enabled = btnServiceBusStartMonitoring.Enabled = false;
            btnServiceBusStopMonitoring.Enabled = true;

            WriteTrace("Monitoring email events started");
        }

        private void btnServiceBusStopMonitoring_Click(object sender, EventArgs e)
        {
            WriteTrace("Stop monitoring email events...");
            if (_serviceBusReceiverManager != null)
            {
                _serviceBusReceiverManager.ServiceBusEvent -= ServiceBusEventHandler;
                _serviceBusReceiverManager.Stop();
                _serviceBusReceiverManager = null;

                pnlServiceBusConfig.Enabled = btnServiceBusStartMonitoring.Enabled = true;
                btnServiceBusStopMonitoring.Enabled = false;
            }

            WriteTrace("Monitoring email events stopped");
        }

        private void ServiceBusEventHandler(object? sender, ServiceBusEventArgs e)
        {
            WriteTrace($"<-- Event received: {Environment.NewLine}===================={Environment.NewLine}{e.Body}{Environment.NewLine}====================");
        }

        private void dgEmailClientConfig_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgEmailClientConfig.CurrentCell.ColumnIndex == dgEmailClientConfigColumnIndex.VALUE)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dgEmailClientConfigIntValueCell_KeyPressEnforceNumbers);
                e.Control.KeyPress -= new KeyPressEventHandler(dgEmailClientConfigIntValueCell_KeyPressReadOnly);

                EmailAuthConfig emailAuthConfig = _emailAuthConfig.First(c => c.Name == dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.NAME].Value.ToString());

                if (emailAuthConfig.IsReadOnly)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(dgEmailClientConfigIntValueCell_KeyPressReadOnly);
                    }
                }
                else if (emailAuthConfig.ValType == EmailAuthConfig.ValueType.Integer)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(dgEmailClientConfigIntValueCell_KeyPressEnforceNumbers);
                    }
                }
            }
        }

        private void dgEmailClientConfigIntValueCell_KeyPressEnforceNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void dgEmailClientConfigIntValueCell_KeyPressReadOnly(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dgEmailClientConfig_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex > -1 && dgEmailClientConfig.CurrentCell.ColumnIndex == dgEmailClientConfigColumnIndex.VALUE)
            {
                EmailAuthConfig emailAuthConfig = _emailAuthConfig.First(c => c.Name == dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.NAME].Value.ToString());

                if (emailAuthConfig.ValType == EmailAuthConfig.ValueType.String && emailAuthConfig.IsPassword)
                {
                    dgEmailClientConfig.CurrentCell.Value = emailAuthConfig.GetStringValue();
                }
            }
        }

        private void dgEmailClientConfig_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgEmailClientConfig.CurrentCell.ColumnIndex == dgEmailClientConfigColumnIndex.VALUE)
            {
                EmailAuthConfig emailAuthConfig = _emailAuthConfig.First(c => c.Name == dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.NAME].Value.ToString());

                if (emailAuthConfig.ValType == EmailAuthConfig.ValueType.String)
                {
                    emailAuthConfig.Value = dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.VALUE].Value;

                    if (emailAuthConfig.IsPassword)
                    {
                        dgEmailClientConfig.CurrentCell.Value = emailAuthConfig.GetMaskedValue();
                    }
                }
                else if (emailAuthConfig.ValType == EmailAuthConfig.ValueType.Integer)
                {
                    emailAuthConfig.Value = dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.VALUE].Value;
                }
                else if (emailAuthConfig.ValType == EmailAuthConfig.ValueType.Boolean)
                {
                    emailAuthConfig.Value = dgEmailClientConfig.Rows[dgEmailClientConfig.CurrentCell.RowIndex].Cells[dgEmailClientConfigColumnIndex.VALUE].Value;
                }
            }
        }
    }
}