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
        public const string ENTRA_ID_CLIENT_ID = "EntraId_ClientId";
        public const string ENTRA_ID_CLIENT_SECRET = "EntraId_ClientSecret";

        public const string SMTP_ENDPOINT = "SmtpEndpoint";
        public const string SMTP_PORT = "SmtpPort";
        public const string SMTP_USERNAME = "SmtpUsername";
        public const string SMTP_PASSWORD = "SmtpPassword";

        public const string SERVICE_BUS_CONNECTION_STRING = "ServiceBusConnectionString";
        public const string SERVICE_BUS_QUEUE_NAME = "ServiceBusQueueName";

        public const string USE_OAUTH2 = "UseOAuth2";
        public const string AUTO_RETRY_ON_429 = "AutoRetryOn429";
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
        public const string AAD_DEFAULT_CREDENTIALS = "EntraIdDefaultCredentials";
        public const string AAD_CLIENT_SECRESTS = "EntraIdClientSecret";
        public const string INTERACTIVE = "Interactive";
        public const string SMTP_USERNAME_PASSWORD = "SmtpUsernamePassword";
    }
}
