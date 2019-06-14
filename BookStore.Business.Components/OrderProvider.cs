using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;
using System.Transactions;
using Common.Model;
using Microsoft.Practices.ServiceLocation;
using DeliveryCo.MessageTypes;
using BookStore.Business.Components.PublisherService;
using BookStore.Business.Components.Transformations;
using BookStore.Business.Entities.Model;

namespace BookStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        public IEmailProvider EmailProvider
        {
            get { return ServiceLocator.Current.GetInstance<IEmailProvider>(); }
        }

        public IUserProvider UserProvider
        {
            get { return ServiceLocator.Current.GetInstance<IUserProvider>(); }
        }

        public void SubmitOrder(Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
                {
                    try
                    {
                        LoadWarehouseStocks(pOrder);
                        FindWarehouse(pOrder);
                        pOrder.Warehouse = lContainer.Warehouses.Where(warehouse => warehouse.Id == pOrder.Warehouse_Id).First().Name;
                        pOrder.Store = "OnLine";
                        pOrder.OrderNumber = Guid.NewGuid();
                        foreach ( OrderItem orderItem in pOrder.OrderItems)
                        {
                            lContainer.OrderItems.Add(orderItem);
                        }
                        lContainer.Orders.Add(pOrder);
                        Console.WriteLine("Order Received: " + pOrder.OrderNumber);
                        TransferFundsFromCustomer(UserProvider.ReadUserById(pOrder.Customer.Id).BankAccountNumber, (double)pOrder.Total, pOrder.OrderNumber, pOrder.Customer.Id);
                        Console.WriteLine("Funds Transfer Requested by Customer:" + pOrder.Customer.Id);

                        lContainer.SaveChanges();
                        lScope.Complete();

                    }
                    catch (Exception lException)
                    {
                        SendOrderErrorMessage(pOrder, lException);
                        throw;
                    }
                }
            }
        }

        public void LoadWarehouseStocks(Order pOrder)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                foreach (OrderItem lOrderItem in pOrder.OrderItems)
                {
                    String Btitle = lOrderItem.Book.Title;

                    int BookId = lContainer.WBooks.Where(wbook => Btitle == wbook.Title).First().Id;
                    foreach (WStock IWstock in lContainer.WStocks.ToList())
                    {
                        if (IWstock.WBook_id == BookId)
                        {
                            lOrderItem.Book.WStocks.Add(IWstock);
                        }
                    }
                }
                lContainer.SaveChanges(); 
            }
        }
        public void FundsTransferSuccessful(Guid pOrderGuid)
        {

            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                try
                {
                    Console.WriteLine("Funds Transfer Successful.");
                    Order pOrder = lContainer.Orders.Include("OrderItems").First(x => x.OrderNumber == pOrderGuid);
                    PlaceDeliveryForOrder(pOrder);

                    lContainer.SaveChanges();
                }
                catch (Exception lException)
                {
                    Console.WriteLine("Error in Placing Delivery Order: " + lException.Message);
                    throw;
                }
            }
        }

        public void FundsTransferError(Guid pOrderGuid)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
                {
                    try
                    {
                        Console.WriteLine("Funds Transfer Error");
                        var pOrder = lContainer.Orders
                            .Include("Customer").FirstOrDefault(x => x.OrderNumber == pOrderGuid);

                        EmailProvider.SendMessage(new EmailMessage()
                        {
                            ToAddress = pOrder.Customer.Email,
                            Message = "You have insufficient funds in your bank account. Your order " + pOrderGuid + " has been cancelled."
                        });
                        lContainer.SaveChanges();
                        lScope.Complete();
                        
                    }
                    catch (Exception lException)
                    {
                        Console.WriteLine("Error in FundsTransferError: " + lException.Message);
                        throw;
                    }
                }
            }
        }

        private void SendOrderErrorMessage(Order pOrder, Exception pException)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "There was an error in processsing your order " + pOrder.OrderNumber + ": " + pException.Message + ". Please contact the BookStore or try again later."
            });
        }

        private void SendOrderPlacedConfirmation(Order pOrder)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "Your order has been placed. Order number: " + pOrder.OrderNumber
            });
        }

        private void PlaceDeliveryForOrder(Order pOrder)
        {
            Console.WriteLine("Requesting delivery for order" + pOrder.OrderNumber + "Contacting DeliveryCo.");
            DeliveryInfoItem lItem = new DeliveryInfoItem()
            {
                OrderNumber = pOrder.OrderNumber,
                SourceAddress = pOrder.Warehouse,
                DestinationAddress = pOrder.Customer.Address,
                DeliveryNotificationAddress = "net.tcp://localhost:9010/DeliveryNotificationService"
            };

            DeliveryInfoItemToDeliveryInfoMessage lVisitor = new DeliveryInfoItemToDeliveryInfoMessage();
            lVisitor.Visit(lItem);
            PublisherServiceClient lClient = new PublisherServiceClient();
            lClient.Publish(lVisitor.Result);
        }

     
        public void DeliverySubmitted(Guid pOrderGuid, Guid pDeliveryGuid)
        {
                using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
                {
                    Console.WriteLine("Delivery for order: " + pOrderGuid + "has been submitted for delivery. Delivery id: " + pDeliveryGuid);
                    var pOrder = lContainer.Orders.Include("Customer").First(x => x.OrderNumber == pOrderGuid);

                    Delivery lDelivery = new Delivery()
                    {
                        DeliveryStatus = DeliveryStatus.Submitted,
                        SourceAddress = "Book Store Address",
                        DestinationAddress = pOrder.Customer.Address,
                        Order = pOrder,
                        ExternalDeliveryIdentifier = pDeliveryGuid
                    };
                    pOrder.Delivery = lDelivery; 
                    lContainer.Deliveries.Add(lDelivery);
                
                //update stocks 
                foreach (OrderItem lOrderItem in pOrder.OrderItems)
                {
                    int book_id = lContainer.WBooks.Where(books => books.Title == lOrderItem.Book.Title).First().Id;
                    WStock lWStock = lContainer.WStocks.Where(stock => stock.Warehouse_id == pOrder.Warehouse_Id).Where(Stock => Stock.WBook_id == book_id).First();
                    lWStock.quantity -= lOrderItem.Quantity;
                }

                    lContainer.SaveChanges();
                    SendOrderPlacedConfirmation(pOrder);
                    
                }
        }

        private void TransferFundsFromCustomer(int pCustomerAccountNumber, double pTotal, Guid pOrderGuid, int pCustomerId)
        {
            try
            {
                RequestFundsTransferItem lItem = new RequestFundsTransferItem
                {
                    Amount = pTotal,
                    FromAccountNumber = pCustomerAccountNumber,
                    ToAccountNumber = RetrieveBookStoreAccountNumber(),
                    OrderGuid = pOrderGuid,
                    CustomerId = pCustomerId
                };
                RequestFundsTransferItemToRequestFundsTransferMessage lVisitor = new RequestFundsTransferItemToRequestFundsTransferMessage();
                lVisitor.Visit(lItem);
                PublisherServiceClient lClient = new PublisherServiceClient();
                lClient.Publish(lVisitor.Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error occurred when attempting to transfer funds for order " + pOrderGuid.ToString());
            }
        }


        private int RetrieveBookStoreAccountNumber()
        {
            return 123;
        }

        private void FindWarehouse(Order pOrder)
        {
            OrderItem first = pOrder.OrderItems.First();
            List<WStock> wstock = first.Book.WStocks.ToList();
            IList<int> intWarehouse = new List<int>();
            foreach (WStock IWStock in wstock)
            {

                intWarehouse.Add(IWStock.Warehouse_id);
            }
            IList<int> warehouses = new List<int>();
            foreach (OrderItem lOrderItem in pOrder.OrderItems)
            {

                List<WStock> wstocks = lOrderItem.Book.WStocks.ToList();

                IList<int> warehouse_id = new List<int>();
                foreach (WStock IWStock in wstocks)
                {

                    if (IWStock.quantity - lOrderItem.Quantity >= 0)
                    {
                        warehouse_id.Add(IWStock.Warehouse_id);
                    }
                    if (!warehouse_id.Any())
                    {
                        pOrder.ThrowInsufficientStockException();
                    }
                }
                warehouses = intWarehouse.Intersect(warehouse_id).ToList();
                if (warehouses == null)
                {
                    break;

                }
            }
            int warehouseId = warehouses.First();
            pOrder.Warehouse_Id = warehouseId;
        }
    }
}
