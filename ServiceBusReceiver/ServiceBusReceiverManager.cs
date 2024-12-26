using Azure.Messaging.ServiceBus;

namespace ServiceBusReceiver
{
    public class ServiceBusReceiverManager
    {
        // the client that owns the connection and can be used to create senders and receivers
        private ServiceBusClient _serviceBusClient;

        // the processor that reads and processes messages from the queue
        private ServiceBusProcessor _serviceBusProcessor;

        private string _connectionString;
        private string _queueName;

        public event EventHandler<ServiceBusEventArgs> ServiceBusEvent;


        public ServiceBusReceiverManager(string serviceBusConnectionSring, string queueName)
        {
            _connectionString = serviceBusConnectionSring;
            _queueName = queueName;
        }

        // handle received messages
        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            //Console.WriteLine($"Received: {body}");

            ServiceBusEvent?.Invoke(this, new ServiceBusEventArgs(body));

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        public async void Start()
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // Set the transport type to AmqpWebSockets so that the ServiceBusClient uses port 443. 
            // If you use the default AmqpTcp, make sure that ports 5671 and 5672 are open.

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            _serviceBusClient = new ServiceBusClient(_connectionString, clientOptions);

            // create a processor that we can use to process the messages
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                _serviceBusProcessor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await _serviceBusProcessor.StartProcessingAsync();

                //Console.WriteLine("Wait for a minute and then press any key to end the processing");

                //await Task.Delay(TimeSpan.FromSeconds(3));

                //// stop processing 
                //Console.WriteLine("\nStopping the receiver...");
                //await processor.StopProcessingAsync();
                //Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                //// Calling DisposeAsync on client types is required to ensure that network
                //// resources and other unmanaged objects are properly cleaned up.
                //await processor.DisposeAsync();
                //await client.DisposeAsync();
            }
        }

        public async void Stop()
        {
            await _serviceBusProcessor.StopProcessingAsync();
            await _serviceBusClient.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }
    }
}
