using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public void SendEmail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending email To: {to} from {from} subject {subject} "); 
        }
    }
}
