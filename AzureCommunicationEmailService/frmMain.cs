using Azure.Communication.Email;
using Azure.Communication.Email.Models;
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
        //private const int ATTACHMENTS_DELETE_COL_INDEX = 3;

        private const int CUSTOM_HEADER_NAME_COL_INDEX = 0;
        private const int CUSTOM_HEADER_VALUE_COL_INDEX = 1;




        //private static object _syncLock = new object();
        //private static ReaderWriterLockSlim _readWriteLockSlim = new ReaderWriterLockSlim();

        private System.Timers.Timer _checkMessageStatusTimer;
        //private List<string> _messageIds = new List<string>();
        //private BlockingCollection<string> _messageIdList = new BlockingCollection<string>();
        private ConcurrentDictionary<string, DateTime> _messageIds = new ConcurrentDictionary<string, DateTime>();
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

            foreach (var customHeader in configWrapper.CustomHeaders)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgCustomHeaders);

                row.Cells[CUSTOM_HEADER_NAME_COL_INDEX].Value = customHeader.Name;
                row.Cells[CUSTOM_HEADER_VALUE_COL_INDEX].Value = customHeader.Value;

                dgCustomHeaders.Rows.Add(row);
            }

            _checkMessageStatusTimer = new System.Timers.Timer(1000);
            _checkMessageStatusTimer.AutoReset = false;
            _checkMessageStatusTimer.Elapsed += CheckMessageStatusTimer_Elapsed;
            _checkMessageStatusTimer.Start();
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

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            WriteTrace("Start sending email...");

            if (_emailClient == null)
            {
                MessageBox.Show("Please fill ACS connection string and select Initialize button", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnString.Focus();
                WriteTrace("Sending failed. Email client is not initialized");
                return;
            }

            EmailContent emailContent = new EmailContent(txtSubject.Text);
            if (chkIsHtmlBody.Checked)
            {
                emailContent.Html = txtBody.Text + $"<br/><hr/><div>Sent at: {DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} UTC</div>";
            }
            else
            {
                emailContent.PlainText = txtBody.Text + Environment.NewLine + $"Sent at: {DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")} UTC";
            }

            List<EmailAddress> receipeintToEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintCCEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintBCCEmailAddresses = new List<EmailAddress>();
            List<EmailAddress> receipeintReplyToEmailAddresses = new List<EmailAddress>();
            for (int i = 0; i < dgReceipeints.RowCount - 1; i++)
            {
                if (dgReceipeints.Rows[i] != null && !string.IsNullOrEmpty(dgReceipeints.Rows[i].Cells[0].ToString().Trim()))
                {
                    string emailAddress = dgReceipeints.Rows[i].Cells[RECEIPEINT_EMAIL_COL_INDEX].Value.ToString();
                    string displayName = dgReceipeints.Rows[i].Cells[RECEIPEINT_DISPLAY_NAME_COL_INDEX].Value.ToString();

                    if (dgReceipeints.Rows[i].Cells[2].Value.Equals(RECEIPEINT_TYPE_CC))
                    {
                        receipeintCCEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[2].Value.Equals(RECEIPEINT_TYPE_BCC))
                    {
                        receipeintBCCEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[2].Value.Equals(RECEIPEINT_TYPE_TO))
                    {
                        receipeintToEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                    else if (dgReceipeints.Rows[i].Cells[2].Value.Equals(RECEIPEINT_TYPE_REPLY_TO))
                    {
                        receipeintReplyToEmailAddresses.Add(new EmailAddress(emailAddress, displayName));
                    }
                }
            }

            EmailRecipients emailRecipients = new EmailRecipients(receipeintToEmailAddresses, receipeintCCEmailAddresses, receipeintBCCEmailAddresses);

            EmailMessage emailMessage = new EmailMessage(txtFrom.Text, emailContent, emailRecipients);

            receipeintReplyToEmailAddresses.ForEach(r => emailMessage.ReplyTo.Add(r));

            // attacment
            long attachmentSize = long.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands);
            for (int i = 0; i < dgAttachments.RowCount; i++)
            {
                string filePath = dgAttachments.Rows[i].Cells[ATTACHMENTS_FILE_PATH_COL_INDEX].Value.ToString();
                string attachmentName = System.IO.Path.GetFileName(filePath);
                EmailAttachmentType emailAttachmentType = new EmailAttachmentType(dgAttachments.Rows[i].Cells[ATTACHMENTS_TYPE_COL_INDEX].Value.ToString());

                byte[] bytes = File.ReadAllBytes(filePath);
                string attachmentFileInBytes = Convert.ToBase64String(bytes);

                EmailAttachment emailAttachment = new EmailAttachment(attachmentName, emailAttachmentType, attachmentFileInBytes);

                emailMessage.Attachments.Add(emailAttachment);
            }
            //End attachment

            //Set email importance
            switch (cmbImportance.Text.ToLower())
            {
                case "low":
                    emailMessage.Importance = EmailImportance.Low;
                    break;

                case "normal":
                    emailMessage.Importance = EmailImportance.Normal;
                    break;

                case "high":
                    emailMessage.Importance = EmailImportance.High;
                    break;
            }

            //Add custom headers
            for (int i = 0; i < dgCustomHeaders.RowCount - 1; i++)
            {
                emailMessage.CustomHeaders.Add(new EmailCustomHeader(dgCustomHeaders.Rows[i].Cells[CUSTOM_HEADER_NAME_COL_INDEX].Value.ToString(), dgCustomHeaders.Rows[i].Cells[CUSTOM_HEADER_VALUE_COL_INDEX].Value.ToString()));
            }

            SendEmailResult sendEmailResult = _emailClient.Send(emailMessage);
            WriteTrace($"Email sent. Attachment count: {emailMessage.Attachments.Count}; Size: {FormatSize(attachmentSize)}. Message Id: {sendEmailResult.MessageId}");

            _messageIds.TryAdd(sendEmailResult.MessageId, DateTime.Now);
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
                txtTrace.AppendText($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] {message}{Environment.NewLine}");
            }
        }

        private void CheckMessageStatusTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (_messageIds.Count == 0)
                {
                    return;
                }
                else if (_emailClient == null)
                {
                    WriteTrace($"Connection string reset. Exit checking send status for message Id(s) {String.Join(", ", _messageIds.ToArray())}");
                    _messageIds.Clear();
                    return;
                }

                foreach (var messageIdKeyValuePair in _messageIds)
                {
                    Azure.Response<SendStatusResult> messageStatus = _emailClient.GetSendStatus(messageIdKeyValuePair.Key);

                    WriteTrace($"Message {messageIdKeyValuePair.Key}; status: {messageStatus.Value.Status}");

                    if (messageStatus.Value.Status == SendStatus.Queued)
                    {
                        _messageIds.TryUpdate(messageIdKeyValuePair.Key, DateTime.Now.AddSeconds(1), messageIdKeyValuePair.Value);
                    }
                    else
                    {
                        DateTime tempTime;
                        _messageIds.Remove(messageIdKeyValuePair.Key, out tempTime);
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
            if ((row.Cells[ATTACHMENTS_TYPE_COL_INDEX] as DataGridViewComboBoxCell).Items.Contains(fileExtension))
            {
                row.Cells[ATTACHMENTS_TYPE_COL_INDEX].Value = fileExtension;
            }

            dgAttachments.Rows.Add(row);
        }

        private void FillAttachments(List<string> filesPath)
        {
            foreach (string filePath in filesPath)
            {
                FillAttachment(filePath);
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
                lbAttachmentsCount.Text = senderGrid.RowCount.ToString();

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
                lbAttachmentsCount.Text = (Convert.ToInt32(lbAttachmentsCount.Text) + 1).ToString();

                lblAttachmentsSize.Text = (int.Parse(lblAttachmentsSize.Text, System.Globalization.NumberStyles.AllowThousands) + Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[ATTACHMENTS_FILES_SIZE_COL_INDEX].Value.ToString(), System.Globalization.NumberStyles.AllowThousands)).ToString("N0");
            }
        }

        private void btnInitializeConnString_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnString.Text.Trim()))
            {
                MessageBox.Show("Please fill connection string", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConnString.Focus();
                return;
            }

            if (_emailClient == null)
            {
                _emailClient = new EmailClient(txtConnString.Text);
                txtConnString.Enabled = false;
                btnInitializeConnString.Enabled = false;
                _checkMessageStatusTimer.Start();
            }
        }
    }
}