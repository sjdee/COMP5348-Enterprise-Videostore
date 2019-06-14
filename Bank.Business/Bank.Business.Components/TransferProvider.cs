using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Components.Interfaces;
using Bank.Business.Entities;
using System.Transactions;
using Bank.Services.Interfaces;
using Bank.Business.Components.PublisherService;
using Common.Model;
using System.Data.Entity; 
using Bank.Business.Components.Model;
using Bank.Business.Components.Transformations;

namespace Bank.Business.Components
{
    public class TransferProvider : ITransferProvider
    {


        public void Transfer(double pAmount, int pFromAcctNumber, int pToAcctNumber, Guid pOrderGuid, int pCustomerId)
        {
            using (TransactionScope lScope = new TransactionScope()) {
                using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
                {
                    try
                    {
                        // find the two account entities and add them to the Container
                        Account lFromAcct = lContainer.Accounts.Where(account => pFromAcctNumber == account.AccountNumber).First();
                        Account lToAcct = lContainer.Accounts.Where(account => pToAcctNumber == account.AccountNumber).First();

                        // update the two accounts
                        lFromAcct.Withdraw(pAmount);
                        lToAcct.Deposit(pAmount);
                        lContainer.Accounts.Attach(lFromAcct);
                        lContainer.Accounts.Attach(lToAcct);
                        lContainer.Entry(lFromAcct).State = EntityState.Modified;
                        lContainer.Entry(lToAcct).State = EntityState.Modified;
                        Console.WriteLine("Funds transfer successful for Order Number " + pOrderGuid.ToString());

                        var lItem = new FundsTransferSuccessful
                        {
                            OrderGuid = pOrderGuid,
                            CustomerId = pCustomerId
                        };
                        var lVisitor = new FundsTransferSuccessfulToMessage();
                        lItem.Accept(lVisitor);
                        PublisherServiceClient lClient = new PublisherServiceClient();
                        lClient.Publish(lVisitor.Result);
                    }
                    catch (Exception lException)
                    {
                        Console.WriteLine("Error occured while transferring money:  " + lException.Message);

                        var lItem = new FundsTransferError
                        {
                            OrderGuid = pOrderGuid,
                            Error = lException
                        };
                        var lVisitor = new FundsTransferErrorToMessage();
                        lItem.Accept(lVisitor);
                        PublisherServiceClient lClient = new PublisherServiceClient();
                        lClient.Publish(lVisitor.Result);
                    }
                    // save changed entities and finish the transaction                 
                    lContainer.SaveChanges();
                    lScope.Complete();
                }
            }
        }

        private Account GetAccountFromNumber(int pToAcctNumber)
        {
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {
                return lContainer.Accounts.Where((pAcct) => (pAcct.AccountNumber == pToAcctNumber)).FirstOrDefault();
            }
        }
    }
}
