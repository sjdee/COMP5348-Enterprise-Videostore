using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;
using Microsoft.Practices.ServiceLocation;
using System.Transactions;

namespace BookStore.Business.Components
{
    public class DeliveryNotificationProvider : IDeliveryNotificationProvider
    {
        public IEmailProvider EmailProvider
        {
            get { return ServiceLocator.Current.GetInstance<IEmailProvider>(); }
        }

        public void NotifyDeliveryStatus(Guid pDeliveryId, Entities.DeliveryStatus status)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Order lAffectedOrder = RetrieveDeliveryOrder(pDeliveryId);
                UpdateDeliveryStatus(pDeliveryId, status);
                if (status == Entities.DeliveryStatus.GoodsPicked)
                {
                    Console.WriteLine("Message from DeliveryCo: Delivery " + pDeliveryId + "has been picked up from one of our warehouses.");
                    EmailProvider.SendMessage(new EmailMessage()
                    {
                        ToAddress = lAffectedOrder.Customer.Email,
                        Message = "Our records show that the goods for your order" + lAffectedOrder.OrderNumber + " have been picked up from the Warehouse and will arrive soon. Thank you for shopping at Book store."
                    });
                }

                if (status == Entities.DeliveryStatus.InTransit)
                {
                    Console.WriteLine("Message from DeliveryCo: Delivery " + pDeliveryId + "is in transit.");
                    EmailProvider.SendMessage(new EmailMessage()
                    {
                        ToAddress = lAffectedOrder.Customer.Email,
                        Message = "Our records show that your order" + lAffectedOrder.OrderNumber + " is in transit and will arrive very soon. Thank you for shopping at Book store."
                    });
                }
                if (status == Entities.DeliveryStatus.Delivered)
                {
                    Console.WriteLine("Message from DeliveryCo: Delivery " + pDeliveryId + "has been delivered. Thank you for using DeliveryCo.");
                    EmailProvider.SendMessage(new EmailMessage()
                    {
                        ToAddress = lAffectedOrder.Customer.Email,
                        Message = "Our records show that your order" + lAffectedOrder.OrderNumber + " has been delivered. Thank you for shopping at Book store."
                    });
                }
                if (status == Entities.DeliveryStatus.Failed)
                {
                    Console.WriteLine("Message from DeliveryCo: Sorry, there was an issue with delivery " + pDeliveryId + ". Please contact DeliveryCo.");
                    EmailProvider.SendMessage(new EmailMessage()
                    {
                        ToAddress = lAffectedOrder.Customer.Email,
                        Message = "Our records show that there was a problem" + lAffectedOrder.OrderNumber + " delivering your order. Please contact Book Store."
                    });
                }
            }
        }

        private void UpdateDeliveryStatus(Guid pDeliveryId, DeliveryStatus status)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Delivery lDelivery = lContainer.Deliveries.Where((pDel) => pDel.ExternalDeliveryIdentifier == pDeliveryId).First();
                if (lDelivery != null)
                {
                    lDelivery.DeliveryStatus = status;
                    lContainer.SaveChanges();
                }
            }
        }

        private Order RetrieveDeliveryOrder(Guid pDeliveryId)
        {
 	        using(BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Delivery lDelivery =  lContainer.Deliveries.Include("Order.Customer").Where((pDel) => pDel.ExternalDeliveryIdentifier == pDeliveryId).First();
                return lDelivery.Order;
            }
        }
    }


}
