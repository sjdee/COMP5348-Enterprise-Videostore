using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Business.Entities.Model;

namespace BookStore.Business.Components.Transformations
{
    public class RequestFundsTransferItemToRequestFundsTransferMessage : IVisitor
    {
        public RequestFundsTransferMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is RequestFundsTransferItem)
            {
                var item = pVisitable as RequestFundsTransferItem;
                Result = new RequestFundsTransferMessage
                {
                    Amount = item.Amount,
                    CustomerId = item.CustomerId,
                    FromAccountNumber = item.FromAccountNumber,
                    ToAccountNumber = item.ToAccountNumber,
                    OrderGuid = item.OrderGuid,
                    Topic = item.Topic
                };
            }
        }

    }
}
