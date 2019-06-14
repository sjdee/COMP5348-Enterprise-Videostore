using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Services.Interfaces;
using BookStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using BookStore.Services.MessageTypes;
using BookStore.Services.MessageTypes.Model; 
using System.ServiceModel;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {

        private IOrderProvider OrderProvider
        {
            get
            {
                return ServiceFactory.GetService<IOrderProvider>();
            }
        }
        public void FundsTransferSuccessful(FundsTransferSuccessfulItem pItem)
        {
            OrderProvider.FundsTransferSuccessful(pItem.OrderGuid);
        }

        public void FundsTransferError(FundsTransferErrorItem pItem)
        {
            OrderProvider.FundsTransferError(pItem.OrderGuid);
        }

        public void DeliverySubmitted(DeliverySubmittedItem pItem)
        {
            
            OrderProvider.DeliverySubmitted(pItem.OrderNumber, pItem.DeliveryId);
        }
        public void SubmitOrder(Order pOrder)
        {
            try
            {
                OrderProvider.SubmitOrder(
                    MessageTypeConverter.Instance.Convert<
                    BookStore.Services.MessageTypes.Order,
                    BookStore.Business.Entities.Order>(pOrder)
                );
            }
            catch(BookStore.Business.Entities.InsufficientStockException ise)
            {
                InsufficientStockFault fault = new InsufficientStockFault() { ItemName = ise.ItemName };
                throw new FaultException<InsufficientStockFault>(fault, new FaultReason(ise.Message)); 
            }
        }
    }
}
