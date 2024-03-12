using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Azure.Identity;
using AzureCommunicationEmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService
{
    public static class Helpers
    {
        public static Version ApplicationVersion { get; set; }

        public static AADCredentials EnvironmentVarCredentials { get; internal set; } = new AADCredentials();

        public static AADCredentials ClientCredentials { get; internal set; } = new AADCredentials();

        public enum CredentialsSource
        {
            AppSettings,
            EnvironmentVariables
        }

        static Helpers()
        {
            EnvironmentVarCredentials.TenantId = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_TENANT_ID) ?? "";
            EnvironmentVarCredentials.ClientId = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_CLIENT_ID) ?? "";
            EnvironmentVarCredentials.ClientSecret = Environment.GetEnvironmentVariable(EnvironmentVariable.AZURE_CLIENT_SECRET) ?? "";
        }

        public static void UpdateClientCredentials(string tenantID, string clientId, string clientSecret)
        {
            ClientCredentials.TenantId = tenantID;
            ClientCredentials.ClientId = clientId;
            ClientCredentials.ClientSecret = clientSecret;
        }

        public static string GetAcsResourceName(string endpoint)
        {
            return endpoint.Trim().Substring(0, endpoint.IndexOf('.')).Substring(endpoint.IndexOf('/') + 2);
        }

        public static EmailClient GetEmailClient(EmailClientConfiguration clientConfiguration)
        {
            EmailClientOptions emailClientOptions = null;
            EmailClient emailClient = null;

            if (clientConfiguration.AutoRetryOn429 == false)
            {
                emailClientOptions = new EmailClientOptions();
                emailClientOptions.AddPolicy(new Catch429Policy(), HttpPipelinePosition.PerRetry);
            }

            if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AcsKey)
            {
                emailClient = new EmailClient(new Uri(clientConfiguration.AcsEndpoint), new AzureKeyCredential(clientConfiguration.AcsKey), emailClientOptions);
            }
            else if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADDefaultCredentials)
            {
                Uri endPoint = new Uri(clientConfiguration.AcsEndpoint);
                emailClient = new EmailClient(endPoint, new DefaultAzureCredential(), emailClientOptions);
            }
            else if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADClientSecrets)
            {
                ClientSecretCredential clientSecretCredential = new ClientSecretCredential(clientConfiguration.Credentials.TenantId, clientConfiguration.Credentials.ClientId, clientConfiguration.Credentials.ClientSecret);
                Uri endPoint = new Uri(clientConfiguration.AcsEndpoint);
                emailClient = new EmailClient(endPoint, clientSecretCredential, emailClientOptions);
            }
            else if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.Interactive)
            {
                Uri endPoint = new Uri(clientConfiguration.AcsEndpoint);
                emailClient = new EmailClient(endPoint, new InteractiveBrowserCredential(), emailClientOptions);
            }

            return emailClient;
        }

        public static string GetFormattedApplicationVersion()
        {
            return Helpers.ApplicationVersion.ToString(3);
        }
    }
}
