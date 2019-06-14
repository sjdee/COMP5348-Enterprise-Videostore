using EmailService.MessageTypes;
using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailService.Services.Transformations
{
    public class SendEmailMessageToEmailMessage : IVisitor
    {
        public EmailMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is SendEmailMessage)
            {
                SendEmailMessage lMsg = pVisitable as SendEmailMessage;
                Result = new EmailMessage()
                {
                    ToAddresses = lMsg.ToAddresses,
                    FromAddresses = lMsg.FromAddresses,
                    CCAddresses = lMsg.CCAddresses,
                    BCCAddresses = lMsg.BCCAddresses,
                    Date = lMsg.Date,
                    Message = lMsg.Message,
                };
            }
        }
    }
}