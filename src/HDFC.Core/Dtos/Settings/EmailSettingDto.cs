using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Core.Dtos.Settings
{
    public class EmailSettingDto
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
