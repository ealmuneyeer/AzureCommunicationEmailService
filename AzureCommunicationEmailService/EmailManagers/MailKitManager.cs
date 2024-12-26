using Azure;
using System.Diagnostics;
using MimeKit;
using AzureCommunicationEmailService.Models;
using MailKit.Net.Smtp;

namespace AzureCommunicationEmailService.EmailManagers
{
    internal class MailKitManager : EmailClientManagerBase
    {
        #region Variables
        internal override string Name { get { return "MailKit client"; } }

        internal override List<EmailClientConfiguration.AuthenticationType> SupportedAuthenticationTypes { get; } = new List<EmailClientConfiguration.AuthenticationType>() { EmailClientConfiguration.AuthenticationType.EntraIdDefaultCredentials, EmailClientConfiguration.AuthenticationType.EntraIdClientSecrets, EmailClientConfiguration.AuthenticationType.SmtpUsernamePassword };

        private string _smtpAuthUsername = "";
        private string _smtpAuthPassword = "";
        #endregion

        #region Internal functions
        internal override bool InternalInitialize()
        {
            if (string.IsNullOrEmpty(ClientConfiguration.SmtpEndpoint))
            {
                WriteTrace($"{Name} initialization failed. SMTP endpoint cannot be empty");
                return false;
            }

            if (ClientConfiguration.SmtpPort < 1)
            {
                WriteTrace($"{Name} initialization failed. Invalid SMTP port number");
                return false;
            }

            _smtpAuthUsername = GetSmtpAuthUsername();
            _smtpAuthPassword = GetSmtpAuthPassword();

            LogInitializationResult(true, null, MessageDeliveryStatusManager?.IsInitialized, _smtpAuthUsername);

            return true;
        }

        internal override void InternalSendEmail(EmailPayload payload)
        {
            using (var mailKitSmtpClient = new SmtpClient())
            {
                mailKitSmtpClient.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                mailKitSmtpClient.Connect(ClientConfiguration.SmtpEndpoint, ClientConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                mailKitSmtpClient.Authenticate(_smtpAuthUsername, _smtpAuthPassword);

                using (MimeMessage mimeMessage = new MimeMessage())
                {
                    mimeMessage.From.Add(MailboxAddress.Parse(payload.From.Address));
                    mimeMessage.Subject = payload.Subject;
                    BodyBuilder bodyBuilder = new BodyBuilder();

                    if (payload.IsBodyHtml)
                    {
                        bodyBuilder.HtmlBody = payload.Body + GetEmailManagerSignatureAsHtml();
                    }
                    else
                    {
                        bodyBuilder.TextBody = payload.Body + GetEmailManagerSignature();
                    }

                    payload.To.ForEach((to) =>
                    {
                        mimeMessage.To.Add(to.ToMailboxAddress());
                    });

                    payload.CC.ForEach((cc) =>
                    {
                        mimeMessage.Cc.Add(cc.ToMailboxAddress());
                    });

                    payload.Bcc.ForEach((bcc) =>
                    {
                        mimeMessage.Bcc.Add(bcc.ToMailboxAddress());
                    });

                    payload.ReplyTo.ForEach((replyTo) =>
                    {
                        mimeMessage.ReplyTo.Add(replyTo.ToMailboxAddress());
                    });


                    //Fill attachments
                    payload.Attachments.ForEach((attachment) =>
                    {
                        if (attachment.IsInline)
                        {
                            var inlineAttachment = bodyBuilder.LinkedResources.Add(attachment.FilePath);
                            inlineAttachment.ContentId = attachment.ContentId;
                        }
                        else
                        {
                            bodyBuilder.Attachments.Add(attachment.FilePath);
                        }
                    });

                    //Set email importance
                    if (payload.Priority > 0)
                    {
                        if (payload.Priority == 1 || payload.Priority == 2)
                        {
                            mimeMessage.Importance = MessageImportance.High;
                        }
                        else if (payload.Priority == 3 || payload.Priority == 4)
                        {
                            mimeMessage.Importance = MessageImportance.Normal;
                        }
                        else
                        {
                            mimeMessage.Importance = MessageImportance.Low;
                        }
                    }

                    //Add custom headers
                    payload.Headers.ForEach((header) =>
                    {
                        mimeMessage.Headers.Add(header.Key, header.Value);
                    });

                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    Stopwatch allEmailsStopwatch = new Stopwatch();
                    Stopwatch singleEmailstopwatch = new Stopwatch();
                    int sentCount = 0;

                    allEmailsStopwatch.Start();

                    //Send multiple emails if required
                    for (int i = 0; i < payload.CountOfEmails; i++)
                    {
                        if (StopSendingEmailsReceived)
                        {
                            WriteTrace("Stop sending emails");
                            break;
                        }

                        singleEmailstopwatch.Restart();

                        string sendResult = mailKitSmtpClient.Send(mimeMessage);

                        singleEmailstopwatch.Stop();

                        sentCount++;

                        WriteTrace($"Email request #{sentCount} has been sent. Result: \"{sendResult}\"; Time elapsed: {singleEmailstopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}; Attachment count: {mimeMessage.Attachments.Count()}; Size: {payload.FormatedAttachmentSize}.");

                        //Extract message ID from result
                        List<string> sendResultInfo = sendResult.Split(' ').ToList();
                        Guid messageId;

                        if (sendResultInfo.Count >= 2 && Guid.TryParse(sendResultInfo[1], out messageId))
                        {
                            MessageDeliveryStatusManager.Monitor(messageId.ToString());
                        }
                    }

                    allEmailsStopwatch.Stop();
                    WriteTrace($"***** {sentCount} email(s) has been sent. Time elapsed: {allEmailsStopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}");
                }
            }
        }
        #endregion
    }
}
