using System;
using System.Collections.Generic;
using Common;
using Common.Model;
using Bank.Services.Transformations;
using Bank.Services.Interfaces; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            if (pMessage is RequestFundsTransferMessage)
            {
                var message = pMessage as RequestFundsTransferMessage;
                var lVisitor = new RequestFundsTransferMessagetoTransferRequest();
                message.Accept(lVisitor);
                var tService = new TransferService();
                tService.Transfer(lVisitor.Result);
            }
        }
    }
}
