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
    public class FundsTransferSuccessfulToMessage : IVisitor
    {
        public FundsTransferSuccessfulMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundsTransferSuccessful)
            {
                var lItem = pVisitable as FundsTransferSuccessful;

                Result = new FundsTransferSuccessfulMessage
                {
                    Topic = lItem.Topic,
                    CustomerId = lItem.CustomerId,
                    OrderGuid = lItem.OrderGuid, 

                };
            }
        }
    }
}
