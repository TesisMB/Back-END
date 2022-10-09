using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class EmailConfiguration
    {
        public string From { get; set; } 
        public string SmtpServer { get; set; }
        public int Port { get; set; } = 465;
        public string UserName { get; set; } 
        public string Password { get; set; } 
    }
}

