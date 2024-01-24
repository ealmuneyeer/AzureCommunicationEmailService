using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Azure.Identity;
using AzureCommunicationEmailService.Models;
using System.Diagnostics;

namespace AzureCommunicationEmailService.EmailManagers
{
    internal class AcsSdkManager : EmailClientManagerBase
    {
        #region Variables
        internal override string Name { get { return "SDK client"; } }

        internal override bool StopSendingEmailsReceived
        {
            get
            {
                return _cancellationToken.IsCancellationRequested;
            }
            set
            {
                if (value == true)
                {
                    _cts.Cancel();
                }
                else
                {
                    _cts.TryReset();
                    _cts.Dispose();
                    _cts = new CancellationTokenSource();
                    _cancellationToken = _cts.Token;
                }

            }
        }

        private EmailClient _emailClient;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private CancellationToken _cancellationToken;
        #endregion

        #region Constructor
        public AcsSdkManager()
        {
            _cancellationToken = _cts.Token;
        }
        #endregion

        #region Internal functions
        internal override bool InternalInitialize()
        {
            if (_emailClient != null)
            {
                _emailClient = null;
            }

            _emailClient = Helpers.GetEmailClient(ClientConfiguration);

            LogInitializationResult(true, ClientConfiguration.AutoRetryOn429, MessageDeliveryStatusManager?.IsInitialized, "n/a");

            return true;
        }

        internal override void InternalSendEmail(EmailPayload payload)
        {
            //Fill email body
            EmailContent emailContent = new EmailContent(payload.Subject);
            if (payload.IsBodyHtml)
            {
                emailContent.Html += payload.Body + GetEmailManagerSignatureAsHtml();
            }
            else
            {
                emailContent.PlainText = payload.Body + GetEmailManagerSignature();
            }

            //Fill Receipents
            EmailRecipients emailRecipients = new EmailRecipients();

            payload.To.ForEach((to) =>
            {
                emailRecipients.To.Add(to.ToEmailAddress());
            });

            payload.CC.ForEach((cc) =>
            {
                emailRecipients.CC.Add(cc.ToEmailAddress());
            });

            payload.Bcc.ForEach((bcc) =>
            {
                emailRecipients.BCC.Add(bcc.ToEmailAddress());
            });

            EmailMessage emailMessage = new EmailMessage(payload.From.Address, emailRecipients, emailContent);

            payload.ReplyTo.ForEach((replyTo) =>
            {
                emailMessage.ReplyTo.Add(replyTo.ToEmailAddress());
            });

            // Fill attacments
            payload.Attachments.ForEach((attachment) =>
            {
                string filePath = attachment.Key;
                string attachmentName = Path.GetFileName(filePath);
                string contentType = attachment.Value;

                byte[] bytes = File.ReadAllBytes(filePath);
                BinaryData attachmentBinaryData = new BinaryData(bytes);

                EmailAttachment emailAttachment = new EmailAttachment(attachmentName, contentType, attachmentBinaryData);

                emailMessage.Attachments.Add(emailAttachment);
            });

            //Set email importance
            if (payload.Priority > 0)
            {
                emailMessage.Headers.Add("x-priority", payload.Priority.ToString());
            }

            //Add custom headers
            payload.Headers.ForEach((header) =>
            {
                emailMessage.Headers.Add(header);
            });

            //Send multiple emails
            Stopwatch allEmailsStopwatch = new Stopwatch();
            Stopwatch singleEmailstopwatch = new Stopwatch();
            EmailSendOperation emailSendOperation;
            int sentCount = 0;

            allEmailsStopwatch.Start();
            for (int i = 0; i < payload.CountOfEmails; i++)
            {
                if (StopSendingEmailsReceived)
                {
                    WriteTrace("Stop sending emails");
                    break;
                }

                singleEmailstopwatch.Restart();

                emailSendOperation = _emailClient.SendAsync(payload.WaitUntil, emailMessage, _cancellationToken).Result;

                singleEmailstopwatch.Stop();

                sentCount++;

                WriteTrace($"Email request #{sentCount} has been sent. Waited until: {payload.WaitUntil}; Time elapsed: {singleEmailstopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}; Attachment count: {emailMessage.Attachments.Count}; Size: {payload.FormatedAttachmentSize}. Message Id: {emailSendOperation?.Id}");

                //Add the send operation to the monitor list
                MessageDeliveryStatusManager?.Monitor(emailSendOperation);
            }

            allEmailsStopwatch.Stop();
            WriteTrace($"***** {sentCount} email(s) has been sent. Waited until: {payload.WaitUntil}; Time elapsed: {allEmailsStopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}");
        }
        #endregion
    }
}
