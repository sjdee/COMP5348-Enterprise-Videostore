using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryCo.Services.Interfaces;
using BookStore.Services.Interfaces;
using BookStore.Services.Transformations;

namespace BookStore.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            var oService = new OrderService();
            var dnService = new DeliveryNotificationService();
            if (pMessage.GetType() == typeof(FundsTransferSuccessfulMessage))
            {
                var lMessage = pMessage as FundsTransferSuccessfulMessage;
                var lVisitor = new FundsTransferSuccessfulMessageToFundsTransferSuccessfulItem();
                lMessage.Accept(lVisitor);
                oService.FundsTransferSuccessful(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(FundsTransferErrorMessage))
            {
                var lMessage = pMessage as FundsTransferErrorMessage;
                var lVisitor = new FundsTransferErrorMessageToFundsTransferErrorItem();
                lMessage.Accept(lVisitor);
                oService.FundsTransferError(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(DeliverySubmittedMessage))
            {
                DeliverySubmittedMessage lMessage = pMessage as DeliverySubmittedMessage;
                var lVisitor = new DeliverySubmittedMessageToDeliverySubmittedItem(); 
                lMessage.Accept(lVisitor);
                oService.DeliverySubmitted(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(DeliveryStatusMessage))
            {
                DeliveryStatusMessage lMessage = pMessage as DeliveryStatusMessage;
                var lVisitor = new DeliveryStatusMessageToDeliveryStatusItem();
                lMessage.Accept(lVisitor);
                dnService.NotifyDeliveryStatus(lVisitor.Result.DeliveryIdentifier, (DeliveryInfoStatus)lVisitor.Result.Status);
            }
        }
    }
}
