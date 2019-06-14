using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Services.MessageTypes.Model;

namespace BookStore.Services.Transformations
{
    public class FundsTransferSuccessfulMessageToFundsTransferSuccessfulItem : IVisitor
    {
        public FundsTransferSuccessfulItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundsTransferSuccessfulMessage)
            {
                var message = pVisitable as FundsTransferSuccessfulMessage;
                Result = new FundsTransferSuccessfulItem
                {
                    OrderGuid = message.OrderGuid
                };
            }
        }
    }
}
