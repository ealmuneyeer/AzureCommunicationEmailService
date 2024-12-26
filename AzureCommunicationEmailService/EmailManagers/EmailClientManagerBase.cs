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

        internal abstract List<EmailClientConfiguration.AuthenticationType> SupportedAuthenticationTypes { get; }

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

            if (!SupportedAuthenticationTypes.Contains(ClientConfiguration.AuthType))
            {
                WriteTrace($"{Name} initialization failed. Unsupported authentication method");
                IsInitialized = false;
            }
            else if (clientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.None)
            {
                WriteTrace($"{Name} initialization failed. Please select authentication method.");
                IsInitialized = false;
            }
            else
            {
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

        internal virtual string GetSmtpAuthUsername()
        {
            if (ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.SmtpUsernamePassword)
            {
                return ClientConfiguration.SmtpUsernamePassword.Username;
            }
            else
            {
                return $"{Helpers.GetAcsResourceName(ClientConfiguration.AcsEndpoint)}|{ClientConfiguration.EntraIdCredentials.ClientId}|{ClientConfiguration.EntraIdCredentials.TenantId}";
            }
        }

        internal virtual string GetSmtpAuthPassword()
        {
            if (ClientConfiguration.AuthType == EmailClientConfiguration.AuthenticationType.SmtpUsernamePassword)
            {
                return ClientConfiguration.SmtpUsernamePassword.Password;
            }
            else
            {
                return ClientConfiguration.EntraIdCredentials.ClientSecret;
            }
        }

        internal virtual string GetEmailManagerSignatureAsHtml()
        {
            return $"<br/><hr/><div>Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC</div><div>Email Communication Services (v{Helpers.GetFormattedApplicationVersion()}) - Sent via {Name}</div>";
        }

        internal virtual string GetEmailManagerSignature()
        {
            return $"{Environment.NewLine}{"".PadLeft(180, '-')} {Environment.NewLine}Sent at: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} UTC {Environment.NewLine}Email Communication Services (v{Helpers.GetFormattedApplicationVersion()}) - Sent via {Name}";
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
