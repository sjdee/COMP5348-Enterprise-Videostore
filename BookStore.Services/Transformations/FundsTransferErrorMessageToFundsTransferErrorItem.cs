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
    public class FundsTransferErrorMessageToFundsTransferErrorItem : IVisitor
    {
        public FundsTransferErrorItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundsTransferErrorMessage)
            {
                var message = pVisitable as FundsTransferErrorMessage;
                Result = new FundsTransferErrorItem
                {
                    OrderGuid = message.OrderGuid
                };
            }
        }
    }
}
