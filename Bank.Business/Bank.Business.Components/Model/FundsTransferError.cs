using Common;
using Common.Model; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Components.Model
{
    public class FundsTransferError : IVisitable
    {
        public String Topic => "FundsTransferError";
        public Exception Error { get; set; }
        public Guid OrderGuid { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
