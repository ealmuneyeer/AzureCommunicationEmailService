using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    public class SmtpUsernamePassword
    {
        public string Username { get; internal set; }

        public string Password { get; internal set; }
    }
}
