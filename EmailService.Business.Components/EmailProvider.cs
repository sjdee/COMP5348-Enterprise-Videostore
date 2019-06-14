using EmailService.Business.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailService.Business.Entities;

namespace EmailService.Business.Components
{
    public class EmailProvider : IEmailProvider
    {
        public void SendEmail(EmailMessage pMessage)
        {
            Console.WriteLine("Sending email to " + pMessage.ToAddresses + ": " + pMessage.Message);
        }
    }
}
