using System;
using Common;

namespace BookStore.Services.MessageTypes.Model
{
    public class DeliveryStatusItem : IVisitable
    {
        public String SourceAddress { get; set; }
        public String DestinationAddress { get; set; }
        public String OrderNumber { get; set; }
        public Guid DeliveryIdentifier { get; set; }
        public String DeliveryNotificationAddress { get; set; }
        public Int32 Status { get; set; }
        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}