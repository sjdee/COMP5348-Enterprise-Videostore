using Bank.Business.Components.Model;
using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Components.Transformations
{
    public class FundsTransferErrorToMessage : IVisitor
    {
        public FundsTransferErrorMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundsTransferError)
            {
                var lItem = pVisitable as FundsTransferError;
                Result = new FundsTransferErrorMessage
                {
                    Topic = lItem.Topic,
                    Error = lItem.Error,
                    OrderGuid = lItem.OrderGuid,
                };
            }
        }
    }
}
