using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    public class CustomAttachment
    {
        public string FilePath { get; set; }

        public string MIMEType { get; set; }

        public string ContentId {  get; set; }

        public bool IsInline { get; set; }
    }
}
