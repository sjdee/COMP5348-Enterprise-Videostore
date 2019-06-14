using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common; 

namespace BookStore.Services.MessageTypes.Model
{
    public class DeliverySubmittedItem : IVisitable
    {
        public Guid OrderNumber { get; set; }
        public Guid DeliveryId { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
