using Azure;
using AzureCommunicationEmailService.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace AzureCommunicationEmailService.EmailManagers
{
    internal class NetMailManager : EmailClientManagerBase
    {
        #region Variables
        internal override string Name { get { return "NetMail client"; } }

        private string _smtpAuthUsername = "";
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

            if (ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADDefaultCredentials || ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADClientSecrets)
            {
                LogAADAuthStatus();
            }
            else
            {
                WriteTrace($"{Name} initialization failed. Unsupported authentication method");
                return false;
            }

            _smtpAuthUsername = GetSmtpAuthUsername();

            LogInitializationResult(true, null, null, _smtpAuthUsername);

            return true;
        }

        internal override void InternalSendEmail(EmailPayload payload)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = ClientConfiguration.SmtpEndpoint;
                smtpClient.Port = ClientConfiguration.SmtpPort;
                smtpClient.Credentials = new NetworkCredential(_smtpAuthUsername, ClientConfiguration.Credentials.ClientSecret);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(payload.From.Address);
                    mailMessage.Subject = payload.Subject;
                    mailMessage.Body = payload.Body;
                    mailMessage.IsBodyHtml = payload.IsBodyHtml;

                    mailMessage.Body += mailMessage.IsBodyHtml ? GetEmailManagerSignatureAsHtml() : GetEmailManagerSignature();

                    payload.To.ForEach((to) =>
                    {
                        mailMessage.To.Add(to);
                    });

                    payload.CC.ForEach((cc) =>
                    {
                        mailMessage.CC.Add(cc);
                    });

                    payload.Bcc.ForEach((bcc) =>
                    {
                        mailMessage.Bcc.Add(bcc);
                    });

                    payload.ReplyTo.ForEach((replyTo) =>
                    {
                        mailMessage.ReplyToList.Add(replyTo);
                    });

                    // Fill attacments 
                    payload.Attachments.ForEach((attachment) =>
                    {
                        Attachment tempAttachment = new Attachment(attachment.FilePath, attachment.MIMEType);

                        if (attachment.IsInline)
                        {
                            tempAttachment.ContentId = attachment.ContentId;
                            tempAttachment.ContentDisposition.Inline = attachment.IsInline;
                        }

                        mailMessage.Attachments.Add(tempAttachment);
                    });

                    //Set email importance
                    if (payload.Priority > 0)
                    {
                        if (payload.Priority == 1 || payload.Priority == 2)
                        {
                            mailMessage.Priority = MailPriority.High;
                        }
                        else if (payload.Priority == 3 || payload.Priority == 4)
                        {
                            mailMessage.Priority = MailPriority.Normal;
                        }
                        else
                        {
                            mailMessage.Priority = MailPriority.Low;
                        }
                    }

                    //Add custom headers
                    payload.Headers.ForEach((header) =>
                    {
                        mailMessage.Headers.Add(header.Key, header.Value);
                    });

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

                        smtpClient.Send(mailMessage);

                        singleEmailstopwatch.Stop();

                        sentCount++;

                        WriteTrace($"Email request #{sentCount} has been sent. Time elapsed: {singleEmailstopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}; Attachment count: {mailMessage.Attachments.Count}; Size: {payload.FormatedAttachmentSize}.");
                    }

                    allEmailsStopwatch.Stop();

                    WriteTrace($"***** {sentCount} email(s) has been sent. Time elapsed: {allEmailsStopwatch.Elapsed.ToString(TIME_ELAPSED_FORMAT)}");
                }
            }
        }
        #endregion
    }
}
