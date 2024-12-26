using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusReceiver
{
    public class ServiceBusEventArgs
    {
        public string Body { get; private set; }

        public ServiceBusEventArgs(string body)
        {
            Body = body;
        }
    }
}
