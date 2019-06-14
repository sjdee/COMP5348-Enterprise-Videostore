using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity; 

namespace Bank.Business.Entities
{
    public partial class Account
    {
        public void Withdraw(double pAmount)
        {
            if (this.Balance < pAmount)
            {
                throw new Exception("Insufficient funds to make withdrawal from " + this.AccountNumber);
            }
            Balance -= pAmount;
        }

        public void Deposit(double pAmount)
        {
            Balance += pAmount;
        }
    }
}
