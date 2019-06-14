using Common;
using Common.Model;
using BookStore.Services.MessageTypes.Model;

namespace BookStore.Services.Transformations
{
    public class DeliveryStatusMessageToDeliveryStatusItem : IVisitor
    {
        public DeliveryStatusItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryStatusMessage)
            {
                DeliveryStatusMessage lMsg = pVisitable as DeliveryStatusMessage;
                Result = new DeliveryStatusItem
                {
                    SourceAddress = lMsg.SourceAddress,
                    DestinationAddress = lMsg.DestinationAddress,
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryIdentifier = lMsg.DeliveryIdentifier,
                    DeliveryNotificationAddress = lMsg.DeliveryNotificationAddress,
                    Status = lMsg.Status
                };
            }
        }
    }
}