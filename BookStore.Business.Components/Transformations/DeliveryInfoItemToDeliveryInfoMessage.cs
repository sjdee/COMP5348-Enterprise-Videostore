using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using BookStore.Business.Entities.Model;

namespace BookStore.Business.Components.Transformations
{
    public class DeliveryInfoItemToDeliveryInfoMessage : IVisitor
    {
        public DeliveryInfoMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryInfoItem)
            {
                DeliveryInfoItem lItem = pVisitable as DeliveryInfoItem;
                Result = new DeliveryInfoMessage
                {
                    SourceAddress = lItem.SourceAddress,
                    DestinationAddress = lItem.DestinationAddress,
                    OrderNumber = lItem.OrderNumber,
                    DeliveryIdentifier = lItem.DeliveryIdentifier,
                    DeliveryNotificationAddress = lItem.DeliveryNotificationAddress,
                    Status = lItem.Status,
                    Topic = lItem.Topic
                };
            }
        }

    }
}
