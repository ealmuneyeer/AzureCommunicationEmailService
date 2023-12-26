using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    public class EmailPayload
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsBodyHtml { get; set; }

        public MailAddress From { get; set; }

        public List<MailAddress> To { get; set; } = new List<MailAddress>();

        public List<MailAddress> CC { get; set; } = new List<MailAddress>();

        public List<MailAddress> Bcc { get; set; } = new List<MailAddress>();

        public List<MailAddress> ReplyTo { get; set; } = new List<MailAddress>();

        public List<KeyValuePair<string, string>> Attachments { get; set; } = new List<KeyValuePair<string, string>>();

        public long AttachmentSize { get; set; }

        public string FormatedAttachmentSize { get; set; }

        public short Priority { get; set; }

        public List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>();
        
        public int CountOfEmails { get; set; }

        public WaitUntil WaitUntil { get; set; }
    }
}
