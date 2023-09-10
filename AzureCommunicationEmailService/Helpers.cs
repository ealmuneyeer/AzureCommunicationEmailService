using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService
{
    public static class Helpers
    {
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
        }

        static Helpers()
        {
            EnvironmentVarCredentials.TenantId = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_TENANT_ID) ?? "";
            EnvironmentVarCredentials.ClientId = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_CLIENT_ID) ?? "";
            EnvironmentVarCredentials.ClientSecret = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_CLIENT_SECRET) ?? "";
        }

        public enum CredentialsSource
        {
            AppSettings,
            EnvironmentVariables
        }

        public static AADCredentials EnvironmentVarCredentials { get; internal set; } = new AADCredentials();
        public static AADCredentials ClientCredentials { get; internal set; } = new AADCredentials();

        public static void UpdateClientCredentials(string tenantID, string clientId, string clientSecret)
        {
            ClientCredentials.TenantId = tenantID;
            ClientCredentials.ClientId = clientId;
            ClientCredentials.ClientSecret = clientSecret;
        }

        public class AADCredentials
        {
            public string TenantId { get; internal set; }

            public string ClientId { get; internal set; }

            public string ClientSecret { get; internal set; }
        }
    }
}
