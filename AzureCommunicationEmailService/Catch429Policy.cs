using Azure.Core.Pipeline;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService
{
    internal class Catch429Policy : HttpPipelineSynchronousPolicy
    {
        public override void OnReceivedResponse(HttpMessage message)
        {
            if (message.Response.Status == 429)
            {
                throw new Exception(message.Response.ToString());
            }
            else
            {
                base.OnReceivedResponse(message);
            }
        }
    }
}
