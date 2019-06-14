using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Model; 
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Components.PublisherService;
using BookStore.Business.Entities.Model;
using Bookstore.Business.Components.Transformations; 

namespace BookStore.Business.Components
{
    public class EmailProvider : IEmailProvider
    {
        public void SendMessage(EmailMessage pMessage)
        {
            PublisherServiceClient lClient = new PublisherServiceClient();
            EmailMessageItem lItem = new EmailMessageItem()
            {
                Date = DateTime.Now,
                Message = pMessage.Message,
                ToAddresses = pMessage.ToAddress
            };
            EmailMessageItemToSendEmailMessage lVisitor = new EmailMessageItemToSendEmailMessage();
            lVisitor.Visit(lItem);
            lClient.Publish(lVisitor.Result);
        }
    }
}
