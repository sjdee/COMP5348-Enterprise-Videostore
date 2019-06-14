using Common;
using Common.Model;
using BookStore.Business.Entities.Model;

namespace Bookstore.Business.Components.Transformations
{
    public class EmailMessageItemToSendEmailMessage : IVisitor
    {
        public SendEmailMessage Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is EmailMessageItem)
            {
                EmailMessageItem lItem = pVisitable as EmailMessageItem;
                Result = new SendEmailMessage()
                {
                    ToAddresses = lItem.ToAddresses,
                    FromAddresses = lItem.FromAddresses,
                    CCAddresses = lItem.CCAddresses,
                    BCCAddresses = lItem.BCCAddresses,
                    Date = lItem.Date,
                    Message = lItem.Message,
                    Topic = lItem.Topic
                };
            }
        }
    }
}