using Bank.MessageTypes;
using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services.Transformations
{
    public class RequestFundsTransferMessagetoTransferRequest : IVisitor
    {
        public TransferServiceRequest Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is RequestFundsTransferMessage)
            {
                var message = pVisitable as RequestFundsTransferMessage;
                Result = new TransferServiceRequest
                {
                    Amount = message.Amount,
                    CustomerId = message.CustomerId,
                    FromAccountNumber = message.FromAccountNumber,
                    OrderGuid = message.OrderGuid,
                    ToAccountNumber = message.ToAccountNumber
                };
            }
        }
    }
}
