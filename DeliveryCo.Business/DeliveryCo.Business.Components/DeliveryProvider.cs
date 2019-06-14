using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Business.Components.Interfaces;
using System.Transactions;
using DeliveryCo.Business.Entities;
using System.Threading;
using DeliveryCo.Business.Components.Model;
using DeliveryCo.Business.Components.PublisherService;
using DeliveryCo.Business.Components.Transformations;
using DeliveryCo.Services.Interfaces;


namespace DeliveryCo.Business.Components
{
    public class DeliveryProvider : IDeliveryProvider
    {
        public void SubmitDelivery(Entities.DeliveryInfo pDeliveryInfo)
        {
            
            using (DeliveryCoEntityModelContainer lContainer = new DeliveryCoEntityModelContainer())
            {
                pDeliveryInfo.DeliveryIdentifier = Guid.NewGuid();
                pDeliveryInfo.Status = 0;
                lContainer.DeliveryInfo.Add(pDeliveryInfo);
                lContainer.SaveChanges();

                DeliverySubmittedInfo lItem = new DeliverySubmittedInfo { OrderNumber = Guid.Parse(pDeliveryInfo.OrderNumber), DeliveryId = pDeliveryInfo.DeliveryIdentifier};
                DeliverySubmittedInfoMessageToDeliveryNotification lVisitor =
                    new DeliverySubmittedInfoMessageToDeliveryNotification(pDeliveryInfo.DeliveryIdentifier);
                lVisitor.Visit(lItem);
                PublisherServiceClient lClient = new PublisherServiceClient();
                lClient.Publish(lVisitor.Result);

                ThreadPool.QueueUserWorkItem(new WaitCallback((pObj) => ScheduleDelivery(pDeliveryInfo)));
                
            }
        }

        private void ScheduleDelivery(DeliveryInfo pDeliveryInfo)
        {
            Console.WriteLine("Delivery Request Submitted. Order" + pDeliveryInfo.OrderNumber+ " is ready for pickup from warehouse: " + pDeliveryInfo.SourceAddress +". Delivering to: " + pDeliveryInfo.DestinationAddress);
            Thread.Sleep(5000);
            //notifying of delivery pick up
            using (TransactionScope lScope = new TransactionScope())
            {
                using (DeliveryCoEntityModelContainer lContainer = new DeliveryCoEntityModelContainer())
                {
                    pDeliveryInfo.Status = 1;
                    lContainer.SaveChanges();

                    DeliveryStatus lItem = new DeliveryStatus { DeliveryInfo = pDeliveryInfo };
                    DeliveryStatusMessageToDeliveryNotification lVisitor = new DeliveryStatusMessageToDeliveryNotification();
                    lVisitor.Visit(lItem);
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(lVisitor.Result);

                    lScope.Complete();
                }
            }
            Console.WriteLine("Delivery picked up from warehouse " + pDeliveryInfo.SourceAddress + ". Delivering to:" + pDeliveryInfo.DestinationAddress);
            Thread.Sleep(5000); 
            //notifying of delivery in transit 
            using (TransactionScope lScope = new TransactionScope())
            {
                using (DeliveryCoEntityModelContainer lContainer = new DeliveryCoEntityModelContainer())
                {
                    pDeliveryInfo.Status = 2;
                    lContainer.SaveChanges();

                    DeliveryStatus lItem = new DeliveryStatus { DeliveryInfo = pDeliveryInfo };
                    DeliveryStatusMessageToDeliveryNotification lVisitor = new DeliveryStatusMessageToDeliveryNotification();
                    lVisitor.Visit(lItem);
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(lVisitor.Result);

                    lScope.Complete();
                }
            }
            Console.WriteLine("Delivery " + pDeliveryInfo.Id + " completed. Books in order " + pDeliveryInfo.OrderNumber + " delivered to " + pDeliveryInfo.DestinationAddress);
            Thread.Sleep(5000);
            using (TransactionScope lScope = new TransactionScope())
            {
                using (DeliveryCoEntityModelContainer lContainer = new DeliveryCoEntityModelContainer())
                {
                    pDeliveryInfo.Status = 3;
                    lContainer.SaveChanges();

                    DeliveryStatus lItem = new DeliveryStatus { DeliveryInfo = pDeliveryInfo };
                    DeliveryStatusMessageToDeliveryNotification lVisitor = new DeliveryStatusMessageToDeliveryNotification();
                    lVisitor.Visit(lItem);
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(lVisitor.Result);

                    lScope.Complete();
                }
            }
        }
    }
}
