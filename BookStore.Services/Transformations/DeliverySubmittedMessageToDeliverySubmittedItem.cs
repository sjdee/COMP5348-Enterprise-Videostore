using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Services.MessageTypes.Model;
using Common;
using Common.Model; 

namespace BookStore.Services.Transformations
{
    public class DeliverySubmittedMessageToDeliverySubmittedItem : IVisitor
    {
        public DeliverySubmittedItem Result; 
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliverySubmittedMessage)
            {
                DeliverySubmittedMessage lMsg = pVisitable as DeliverySubmittedMessage;
                Result = new DeliverySubmittedItem
                {
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryId = lMsg.DeliveryId
                };
            }
        }
    }
}
