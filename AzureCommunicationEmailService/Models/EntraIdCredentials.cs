using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    public class EntraIdCredentials
    {
        public string TenantId { get; internal set; }

        public string ClientId { get; internal set; }

        public string ClientSecret { get; internal set; }
    }
}
