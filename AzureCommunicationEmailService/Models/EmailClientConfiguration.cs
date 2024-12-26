using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    public class EmailClientConfiguration
    {
        public enum AuthenticationType
        {
            None,
            AcsKey,
            EntraIdDefaultCredentials,
            EntraIdClientSecrets,
            Interactive,
            SmtpUsernamePassword
        }

        public EmailClientConfiguration(AuthenticationType authenticationType, string acsEndpoint, string acsKey, string smtpEndpoint, int smtpPort, bool autoRetryOn429,
            EntraIdCredentials entraIdCredentials, SmtpUsernamePassword smtpUsernamePassword)
        {
            AuthType = authenticationType;
            AcsEndpoint = acsEndpoint;
            AcsKey = acsKey;
            SmtpEndpoint = smtpEndpoint;
            SmtpPort = smtpPort;
            AutoRetryOn429 = autoRetryOn429;
            EntraIdCredentials = entraIdCredentials;
            SmtpUsernamePassword = smtpUsernamePassword;
        }

        public AuthenticationType AuthType { get; private set; }

        public string AcsEndpoint { get; private set; }

        public string AcsKey { get; private set; }

        public string SmtpEndpoint { get; private set; }

        public int SmtpPort { get; private set; }

        public bool AutoRetryOn429 { get; private set; }

        public EntraIdCredentials EntraIdCredentials { get; private set; }

        public SmtpUsernamePassword SmtpUsernamePassword { get; private set; }
    }
}
