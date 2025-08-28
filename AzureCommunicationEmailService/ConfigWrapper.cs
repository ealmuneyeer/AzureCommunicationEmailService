using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AzureCommunicationEmailService.Models;
using Microsoft.Extensions.Configuration;

namespace AzureCommunicationEmailService
{
    internal class ConfigWrapper
    {
        private readonly IConfiguration _config;

        public ConfigWrapper(IConfiguration config)
        {
            _config = config;

            To = ParseEmailList(_config[AppConfigKeys.TO]);
            CC = ParseEmailList(_config[AppConfigKeys.CC]);
            BCC = ParseEmailList(_config[AppConfigKeys.BCC]);
            ReplyTo = ParseEmailList(_config[AppConfigKeys.REPLY_TO]);

            CustomHeaders = ParseCustomHeaders(_config[AppConfigKeys.CUSTOM_HEADERS]);

            Importance = GetEmailImportance(_config[AppConfigKeys.IMPORTANCE]);

            Attachments = ParseAttachmentPaths(_config[AppConfigKeys.ATTACHMENTS]);

            SmtpPort = string.IsNullOrEmpty(_config[AppConfigKeys.SMTP_PORT]) ? 0 : Convert.ToInt32(_config[AppConfigKeys.SMTP_PORT]);
        }

        private List<MailAddress> ParseEmailList(string emailList)
        {
            List<MailAddress> result = new List<MailAddress>();

            if (!string.IsNullOrEmpty(emailList.Trim()))
            {
                string[] emailArray = emailList.Split(';').Select(e => e.Trim()).ToArray();
                string[] contactInfo;

                foreach (var contact in emailArray)
                {
                    contactInfo = contact.Split(',').Select(i => i.Trim()).ToArray();
                    if (contactInfo.Length == 2)
                    {
                        result.Add(new MailAddress(contactInfo[1].Trim(), contactInfo[0].Trim()));
                    }
                }
            }

            return result;
        }

        private List<CustomHeader> ParseCustomHeaders(string customHeaders)
        {
            List<CustomHeader> result = new List<CustomHeader>();

            if (!string.IsNullOrEmpty(customHeaders))
            {
                string[] customHeaderArray = customHeaders.Split(';').Select(h => h.Trim()).ToArray();
                string[] nameValuePair;

                foreach (var customHeader in customHeaderArray)
                {
                    nameValuePair = customHeader.Split('=').Select(v => v.Trim()).ToArray();
                    if (nameValuePair.Length == 2)
                    {
                        result.Add(new CustomHeader(nameValuePair[0], nameValuePair[1]));
                    }
                }
            }

            return result;
        }

        private int? GetEmailImportance(string importance)
        {
            return (!string.IsNullOrEmpty(importance) ? Convert.ToInt32(importance) : null);
        }

        private List<CustomAttachment> ParseAttachmentPaths(string attachmentPaths)
        {
            List<CustomAttachment> result = new List<CustomAttachment>();

            foreach (var attachement in attachmentPaths.Split(';'))
            {
                if (!string.IsNullOrEmpty(attachement.Trim()))
                {
                    var attachmentVal = attachement.Split(',').Select(i => i.Trim()).ToArray();

                    if (attachmentVal.Length == 2)
                    {
                        result.Add(new CustomAttachment() { FilePath = attachmentVal[0].Trim(), ContentId = attachmentVal[1].Trim(), IsInline = true });
                    }
                    else
                    {
                        result.Add(new CustomAttachment() { FilePath = attachmentVal[0].Trim() });
                    }
                }
            }

            return result;
        }

        public string Subject
        {
            get { return _config[AppConfigKeys.SUBJECT]; }
        }

        public string Body
        {
            get { return _config[AppConfigKeys.BODY]; }
        }

        public string From
        {
            get { return _config[AppConfigKeys.FROM]; }
        }

        public List<MailAddress> To { get; private set; }

        public List<MailAddress> CC { get; private set; }

        public List<MailAddress> BCC { get; private set; }

        public List<MailAddress> ReplyTo { get; private set; }

        public string HtmlBody
        {
            get { return _config[AppConfigKeys.HTML_BODY]; }
        }

        public bool UseHtmlBody
        {
            get
            {
                bool useHtmlBody;
                bool.TryParse(_config[AppConfigKeys.USE_HTML_BODY], out useHtmlBody);
                return useHtmlBody;
            }
        }

        public List<CustomAttachment> Attachments { get; private set; }

        public int? Importance { get; private set; }

        public List<CustomHeader> CustomHeaders { get; private set; }

        public string AuthenticationType
        {
            get { return _config[AppConfigKeys.AUTHENTICATION_TYPE]; }
        }

        public string TenantId
        {
            get { return _config[AppConfigKeys.TENANT_ID]; }
        }

        public string ENTRA_ID_ClientId
        {
            get { return _config[AppConfigKeys.ENTRA_ID_CLIENT_ID]; }
        }

        public string ENTRA_ID_ClientSecret
        {
            get { return _config[AppConfigKeys.ENTRA_ID_CLIENT_SECRET]; }
        }

        public string SmtpEndpoint
        {
            get { return _config[AppConfigKeys.SMTP_ENDPOINT]; }
        }

        public string ACSEndpoint
        {
            get { return _config[AppConfigKeys.ACS_ENDPOINT]; }
        }

        public string ACSAccessKey
        {
            get { return _config[AppConfigKeys.ACS_ACCESS_KEY]; }
        }

        public int SmtpPort { get; private set; }

        public string SmtpUsername
        {
            get { return _config[AppConfigKeys.SMTP_USERNAME]; }
        }

        public string SmtpPassword
        {
            get { return _config[AppConfigKeys.SMTP_PASSWORD]; }
        }

        public string ServiceBusConnectionString
        {
            get { return _config[AppConfigKeys.SERVICE_BUS_CONNECTION_STRING]; }
        }

        public string ServiceBusQueueName
        {
            get { return _config[AppConfigKeys.SERVICE_BUS_QUEUE_NAME]; }
        }

        public bool UseOAuth2
        {
            get
            {
                bool useOAuth2;
                bool.TryParse(_config[AppConfigKeys.USE_OAUTH2], out useOAuth2);
                return useOAuth2;
            }
        }

        public bool AutoRetryOn429
        {
            get
            {
                bool autoRetryOn429;
                bool.TryParse(_config[AppConfigKeys.AUTO_RETRY_ON_429], out autoRetryOn429);
                return autoRetryOn429;
            }
        }

    }
}
