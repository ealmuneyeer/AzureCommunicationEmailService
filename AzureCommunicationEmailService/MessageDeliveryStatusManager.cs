using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Azure.Identity;
using AzureCommunicationEmailService.EmailManagers;
using AzureCommunicationEmailService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static AzureCommunicationEmailService.frmMain;

namespace AzureCommunicationEmailService
{
    internal class MessageDeliveryStatusManager
    {
        #region Variables
        private const int TIMER_INTERVAL = 1000;

        public EmailClient _emailClient = null;

        public bool IsInitialized { get; private set; } = false;

        private System.Timers.Timer _checkMessageStatusTimer;
        private ConcurrentDictionary<EmailSendOperation, DateTime> _messages = new ConcurrentDictionary<EmailSendOperation, DateTime>(); //Key: EmailSendOperation, value: when to check for update

        WriteTraceDelegate WriteTrace;
        WriteExceptionDelegate WriteException;
        #endregion

        public bool Initialize(EmailClientConfiguration clientConfiguration, WriteTraceDelegate writeTraceDelegate, WriteExceptionDelegate writeExceptionDelegate)
        {
            WriteTrace = writeTraceDelegate;
            WriteException = writeExceptionDelegate;

            WriteTrace("Initializing message delivery status manager...");

            if (string.IsNullOrEmpty(clientConfiguration.AcsEndpoint))
            {
                WriteTrace("ACS endpoint cannot be empty. Initialization failed");

                return false;
            }

            if (_emailClient != null)
            {
                _emailClient = null;
            }

            _emailClient = Helpers.GetEmailClient(clientConfiguration);

            WriteTrace($"Message delivery status manager initialization succeeded");

            IsInitialized = true;

            _checkMessageStatusTimer = new System.Timers.Timer(TIMER_INTERVAL);
            _checkMessageStatusTimer.AutoReset = false;
            _checkMessageStatusTimer.Elapsed += CheckMessageStatusTimer_Elapsed;
            _checkMessageStatusTimer.Start();

            WriteTrace = writeTraceDelegate;

            return IsInitialized;
        }

        private async void CheckMessageStatusTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_messages.Count == 0)
                {
                    return;
                }
                else if (_emailClient == null)
                {
                    WriteTrace($"Failed to initialize email client. Exit checking send status for message Id(s) {String.Join(", ", _messages.Select(m => m.Key.Id).ToArray())}");
                    _messages.Clear();
                    return;
                }

                foreach (var message in _messages)
                {
                    try
                    {
                        await message.Key.UpdateStatusAsync();

                        WriteTrace($"Message {message.Key.Id}; status: {(message.Key.HasValue ? message.Key.Value.Status : "n/a")}");

                        if (message.Key.HasCompleted)
                        {
                            DateTime tempTime;
                            _messages.Remove(message.Key, out tempTime);
                            break;
                        }
                    }
                    catch (RequestFailedException ex)
                    {
                        WriteTrace($"Exception occureed while retrieving Message {message.Key.Id} delivery status with error code {ex.ErrorCode} {Environment.NewLine}" +
                                        $"================================================== {Environment.NewLine}" +
                                        $"{ex.Message}" +
                                        $"{Environment.NewLine}==================================================");

                        WriteTrace($"Removing message {message.Key.Id} from monitoring queue");
                        DateTime tempTime;
                        _messages.Remove(message.Key, out tempTime);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("x-ms-error-code: TooManyRequests", StringComparison.InvariantCultureIgnoreCase))
                        {
                            WriteException(ex);

                            WriteTrace($"Throttling limits recieved. Failed to get message {message.Key.Id} status. Messages will be dropped, please try after the retry-after period");

                            DateTime tempTime;
                            _messages.Remove(message.Key, out tempTime);
                        }
                    }
                }
            }
            finally
            {
                _checkMessageStatusTimer.Start();
            }
        }

        public void CheckDeliveryStatus(string messageID)
        {
            WriteTrace($"Getting email delivery status for message {messageID}...");

            //Validate email client
            if (_emailClient == null)
            {
                WriteTrace($"Getting email delivery status failed. Email client initialization failed");
            }

            EmailSendOperation emailSendOperation = null;

            try
            {
                emailSendOperation = new EmailSendOperation(messageID, _emailClient);

                var response = emailSendOperation.UpdateStatusAsync().Result;

                WriteTrace($"Message {emailSendOperation.Id}; status: {(emailSendOperation.HasValue ? emailSendOperation.Value.Status : "n/a")}");
            }
            catch (RequestFailedException ex)
            {
                WriteTrace($"Exception occureed while retrieving message {emailSendOperation.Id} delivery status with error code {ex.ErrorCode} {Environment.NewLine}" +
                                $"================================================== {Environment.NewLine}" +
                                $"{ex.Message}" +
                                $"{Environment.NewLine}==================================================");
            };
        }

        public bool Monitor(string messageID)
        {
            return _messages.TryAdd(new EmailSendOperation(messageID.ToString(), _emailClient), DateTime.Now);
        }

        public bool Monitor(EmailSendOperation emailSendOperation)
        {
            return _messages.TryAdd(emailSendOperation, DateTime.Now);
        }
    }
}
