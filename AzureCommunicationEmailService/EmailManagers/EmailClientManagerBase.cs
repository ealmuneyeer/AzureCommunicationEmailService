using Azure.Communication.Email;
using Azure.Core;
using Azure.Identity;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AzureCommunicationEmailService.frmMain;
using System.Diagnostics;
using AzureCommunicationEmailService.Models;
using System.Net.Mail;

namespace AzureCommunicationEmailService.EmailManagers
{
    internal abstract class EmailClientManagerBase
    {
        #region Variables
        internal const string TIME_ELAPSED_FORMAT = "mm\\:ss\\.fff";

        internal virtual bool StopSendingEmailsReceived { get; set; }

        public bool? IsInitialized { get; private set; } = null;

        internal string SmtpAuthUsername { get; private set; }

        internal MessageDeliveryStatusManager MessageDeliveryStatusManager { get; private set; }

        internal EmailClientConfiguration ClientConfiguration { get; private set; }

        internal WriteTraceDelegate WriteTrace { get; private set; }

        internal WriteExceptionDelegate WriteException { get; private set; }
        #endregion

        #region Abstract
        internal abstract string Name { get; }

        internal abstract bool InternalInitialize();

        internal abstract void InternalSendEmail(EmailPayload payload);
        #endregion

        #region Public functions
        public virtual bool Initialize(EmailClientConfiguration clientConfiguration, MessageDeliveryStatusManager messageDeliveryStatusManager, WriteTraceDelegate writeTraceDelegate, WriteExceptionDelegate writeExceptionDelegate)
        {
            writeTraceDelegate($"Initializing {Name}...");

            if (IsInitialized != null)
            {
                WriteTrace($"{Name} is already initialized");
                return IsInitialized.Value;
            }

            WriteTrace = writeTraceDelegate;
            WriteException = writeExceptionDelegate;

            MessageDeliveryStatusManager = messageDeliveryStatusManager;

            ClientConfiguration = clientConfiguration;

            if (string.IsNullOrEmpty(clientConfiguration.AcsEndpoint))
            {
                WriteTrace($"{Name} initialization failed. ACS endpoint cannot be empty.");
                IsInitialized = false;
            }
            else if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.None)
            {
                WriteTrace($"{Name} initialization failed. Please select authentication method.");
                IsInitialized = false;
            }
            else
            {
                LogAADAuthStatus();
                IsInitialized = InternalInitialize();
            }

            return IsInitialized.Value;
        }

        public virtual bool SendEmail(EmailPayload payload)
        {
            StopSendingEmailsReceived = false;

            WriteTrace($"Start sending email via {Name}...");

            Task.Run(new Action(() =>
            {
                InternalSendEmail(payload);
            })).ContinueWith((Task exceptionTask) =>
            {
                if (exceptionTask.Exception != null)
                {
                    WriteTrace("Exception occureed. Stop sending email(s)");
                    WriteException(exceptionTask.Exception);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);

            return true;
        }

        public virtual void StopSendingEmails()
        {
            WriteTrace("Stop sending emails recieved...");

            StopSendingEmailsReceived = true;
        }
        #endregion

        #region Internal functions
        internal virtual void LogAADAuthStatus()
        {
            if (ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADDefaultCredentials)
            {
                string missingConfigErrorMsg = "Initialization will continue with wanring. Environment varilbale {0} is missing or empty!";

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.TenantId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, EnvironmentVariable.AZURE_TENANT_ID));
                }

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.ClientId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, EnvironmentVariable.AZURE_CLIENT_ID));
                }

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.ClientSecret.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, EnvironmentVariable.AZURE_CLIENT_SECRET));
                }
            }
            else if (ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.AADClientSecrets)
            {
                string missingConfigErrorMsg = "Initialization will continue with wanring. {0} is empty!";

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.TenantId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "Tenant Id"));
                }

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.ClientId.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "AAD_ClientId"));
                }

                if (string.IsNullOrEmpty(ClientConfiguration.Credentials.ClientSecret.Trim()))
                {
                    WriteTrace(string.Format(missingConfigErrorMsg, "AAD_ClientSecret"));
                }
            }
        }

        internal virtual string GetSmtpAuthUsername()
        {
            return $"{Helpers.GetAcsResourceName(ClientConfiguration.AcsEndpoint)}|{ClientConfiguration.Credentials.ClientId}|{ClientConfiguration.Credentials.TenantId}";
        }

        internal virtual string GetEmailManagerSignatureAsHtml()
        {
            return $"<br/><hr/><div>Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC</div><div>Email Communication Services (v{Helpers.ApplicationVersion.ToString(2)}) - Sent via {Name}</div>";
        }

        internal virtual string GetEmailManagerSignature()
        {
            return $"{Environment.NewLine}{"".PadLeft(180, '-')} {Environment.NewLine}Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC {Environment.NewLine}Email Communication Services (v{Helpers.ApplicationVersion.ToString(2)}) - Sent via {Name}";
        }

        internal virtual void LogInitializationResult(bool initializationSucceeded, bool? autoRetryEnabled, bool? monitorMsgDelivery, string username)
        {
            if (initializationSucceeded == false)
            {
                WriteTrace($"{Name} initialization failed");
            }
            else
            {
                WriteTrace($"{Name} initialization succeeded. 429 auto retry enabled: {(autoRetryEnabled.HasValue ? autoRetryEnabled.ToString() : "n/a")}; Monitoring sent message status enabled: {(monitorMsgDelivery.HasValue ? autoRetryEnabled.ToString() : "n/a")}; Username: {username}");
            }
        }
        #endregion

    }
}
