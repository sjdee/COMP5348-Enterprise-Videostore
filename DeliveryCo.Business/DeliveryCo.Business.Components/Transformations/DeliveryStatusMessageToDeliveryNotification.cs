using System;
using Common;
using Common.Model;
using DeliveryCo.Business.Components.Model;

namespace DeliveryCo.Business.Components.Transformations
{
    public class DeliveryStatusMessageToDeliveryNotification : IVisitor
    {
        public DeliveryStatusMessage Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryStatus)
            {
                DeliveryStatus lMsg = pVisitable as DeliveryStatus;
                Result = new DeliveryStatusMessage()
                {
                    SourceAddress = lMsg.DeliveryInfo.SourceAddress,
                    DestinationAddress = lMsg.DeliveryInfo.DestinationAddress,
                    OrderNumber = lMsg.DeliveryInfo.OrderNumber,
                    DeliveryIdentifier = lMsg.DeliveryInfo.DeliveryIdentifier,
                    DeliveryNotificationAddress = lMsg.DeliveryInfo.DeliveryNotificationAddress,
                    Status = lMsg.DeliveryInfo.Status,
                    Topic = lMsg.Topic
                };
            }
        }
    }
}