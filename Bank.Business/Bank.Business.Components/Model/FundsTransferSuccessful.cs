using Common;
using Common.Model; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Components.Model
{
    public class FundsTransferSuccessful : IVisitable
    {
        public String Topic => "FundsTransferSuccessful";
        public Guid OrderGuid { get; set; }
        public int CustomerId { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
