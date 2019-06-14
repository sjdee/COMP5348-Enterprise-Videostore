using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Business.Entities
{
    public partial class Order
    {
        public void ThrowInsufficientStockException()
        {
            throw new InsufficientStockException { ItemName = "Cannot place an order - This book is out of stock" };
        }
    }
}
