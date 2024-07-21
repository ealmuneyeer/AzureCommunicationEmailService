using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService
{
    public class AppConfigKeys
    {
        public const string ACS_ENDPOINT = "ACSEndpoint";
        public const string ACS_ACCESS_KEY = "ACSAccessKey";
        public const string SUBJECT = "Subject";

        public const string FROM = "From";
        public const string TO = "To";
        public const string CC = "CC";
        public const string BCC = "BCC";
        public const string REPLY_TO = "ReplyTo";

        public const string IMPORTANCE = "Importance";
        public const string CUSTOM_HEADERS = "CustomHeaders";

        public const string BODY = "Body";
        public const string HTML_BODY = "HtmlBody";
        public const string USE_HTML_BODY = "UseHtmlBody";

        public const string ATTACHMENTS = "Attachments";

        public const string AUTHENTICATION_TYPE = "AuthenticationType";
        public const string TENANT_ID = "TenantId";
        public const string AAD_CLIENT_ID = "AAD_ClientId";
        public const string AAD_CLIENT_SECRET = "AAD_ClientSecret";

        public const string SMTP_ENDPOINT = "SmtpEndpoint";
        public const string SMTP_PORT = "SmtpPort";
    }

    public class EnvironmentVariable
    {
        public const string AZURE_TENANT_ID = "AZURE_TENANT_ID";
        public const string AZURE_CLIENT_ID = "AZURE_CLIENT_ID";
        public const string AZURE_CLIENT_SECRET = "AZURE_CLIENT_SECRET";
    }

    public class AuthenticationType
    {
        public const string ACS_KEY = "AcsKey";
        public const string AAD_DEFAULT_CREDENTIALS = "AadDefaultCredentials";
        public const string AAD_CLIENT_SECRESTS = "AADClientSecret";
        public const string INTERACTIVE = "Interactive";
    }
}
