using Azure.Communication.Email;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService
{
    internal static class Extensions
    {
        public static MailboxAddress ToMailboxAddress(this MailAddress mailAddress)
        {
            return new MailboxAddress(mailAddress.DisplayName, mailAddress.Address);
        }

        public static EmailAddress ToEmailAddress(this MailAddress mailAddress)
        {
            return new EmailAddress(mailAddress.Address, mailAddress.DisplayName);
        }
    }
}
