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
            AADDefaultCredentials,
            AADClientSecrets,
            Interactive,
        }

        public EmailClientConfiguration(AuthenticationType authenticationType, string acsEndpoint, string acsKey, string smtpEndpoint, int smtpPort, bool autoRetryOn429,
            AADCredentials credentials)
        {
            AuthType = authenticationType;
            AcsEndpoint = acsEndpoint;
            AcsKey = acsKey;
            SmtpEndpoint = smtpEndpoint;
            SmtpPort = smtpPort;
            AutoRetryOn429 = autoRetryOn429;
            Credentials = credentials;
        }

        public AuthenticationType AuthType { get; private set; }

        public string AcsEndpoint { get; private set; }

        public string AcsKey { get; private set; }

        public string SmtpEndpoint { get; private set; }

        public int SmtpPort { get; private set; }

        public bool AutoRetryOn429 { get; private set; }

        public AADCredentials Credentials { get; private set; }
    }
}
