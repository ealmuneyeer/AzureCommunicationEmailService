using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Communication.Email.Models;
using Microsoft.Extensions.Configuration;

namespace AzureCommunicationEmailService
{
    internal class ConfigWrapper
    {
        private readonly IConfiguration _config;
        private List<ConfigEmailAddress> _to;
        private List<ConfigEmailAddress> _cc;
        private List<ConfigEmailAddress> _bcc;
        private List<ConfigEmailAddress> _replyTo;

        private List<CustomHeader> _customHeaders;

        private List<string> _attachmentPaths;

        private EmailImportance? _importance = null;


        public ConfigWrapper(IConfiguration config)
        {
            _config = config;

            _to = ParseEmailList(_config["To"]);
            _cc = ParseEmailList(_config["CC"]);
            _bcc = ParseEmailList(_config["BCC"]);
            _replyTo = ParseEmailList(_config["ReplyTo"]);

            _customHeaders = ParseCustomHeaders(_config["CustomHeaders"]);

            _importance = GetEmailImportance(_config["Importance"]);

            _attachmentPaths = ParseAttachmentPaths(_config["Attachments"]);
        }

        private List<ConfigEmailAddress> ParseEmailList(string emailList)
        {
            List<ConfigEmailAddress> result = new List<ConfigEmailAddress>();

            if (!String.IsNullOrEmpty(emailList.Trim()))
            {
                string[] emailArray = emailList.Split(';').Select(e => e.Trim()).ToArray();
                string[] contactInfo;

                foreach (var contact in emailArray)
                {
                    contactInfo = contact.Split(',').Select(i => i.Trim()).ToArray();
                    if (contactInfo.Length == 2)
                    {
                        result.Add(new ConfigEmailAddress(contactInfo[0].Trim(), contactInfo[1].Trim()));
                    }
                }
            }

            return result;
        }

        private List<CustomHeader> ParseCustomHeaders(string customHeaders)
        {
            List<CustomHeader> result = new List<CustomHeader>();

            if (!String.IsNullOrEmpty(customHeaders))
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

        private EmailImportance? GetEmailImportance(string importance)
        {
            EmailImportance? result = null;

            if (importance.Equals("low", StringComparison.InvariantCultureIgnoreCase))
            {
                result = EmailImportance.Low;
            }
            else if (importance.Equals("normal", StringComparison.InvariantCultureIgnoreCase))
            {
                result = EmailImportance.Normal;
            }
            else if (importance.Equals("high", StringComparison.InvariantCultureIgnoreCase))
            {
                result = EmailImportance.High;
            }

            return result;
        }

        private List<string> ParseAttachmentPaths(string attachmentPaths)
        {
            List<string> result = new List<string>();

            foreach (var path in attachmentPaths.Split(';'))
            {
                if (!string.IsNullOrEmpty(path.Trim()))
                {
                    result.Add(path.Trim());
                }
            }

            return result;
        }

        public string ConnectionString
        {
            get { return _config["ConnectionString"]; }
        }

        public string Subject
        {
            get { return _config["Subject"]; }
        }

        public string Body
        {
            get { return _config["Body"]; }
        }

        public string From
        {
            get { return _config["From"]; }
        }

        public List<ConfigEmailAddress> To
        {
            get { return _to; }
        }

        public List<ConfigEmailAddress> CC
        {
            get { return _cc; }
        }

        public List<ConfigEmailAddress> BCC
        {
            get { return _bcc; }
        }

        public List<ConfigEmailAddress> ReplyTo
        {
            get { return _replyTo; }
        }

        public string HtmlBody
        {
            get { return _config["HtmlBody"]; }
        }

        public bool UseHtmlBody
        {
            get
            {
                bool useHtmlBody;
                bool.TryParse(_config["UseHtmlBody"], out useHtmlBody);
                return useHtmlBody;
            }
        }

        public List<string> Attachments
        {
            get { return _attachmentPaths; }
        }

        public EmailImportance? Importance
        {
            get { return _importance; }
        }

        public List<CustomHeader> CustomHeaders
        {
            get { return _customHeaders; }
        }
    }

    internal class ConfigEmailAddress
    {
        public ConfigEmailAddress(string displayName, string email)
        {
            DisplayName = displayName;
            Email = email;
        }

        public string DisplayName { get; private set; }

        public string Email { get; private set; }
    }

    internal class CustomHeader
    {
        public CustomHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }
    }
}
