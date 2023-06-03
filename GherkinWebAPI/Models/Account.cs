using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Models
{
    public class Account
    {
        public int accountId { get; set; }
        public string accountName { get; set; }
        public int customerId { get; set; }
        public string accountType { get; set; }
        public double accountBalance { get; set; }
        public string accountDesc { get; set; }

        //public List<Customer> Customers { get; set; }

        public static List<Account> GetAllAccounts()
        {
            List<Account> lstAccount = new List<Account>();

            lstAccount.Add(new Account() { accountId = 1, accountName = "Citi Bank Account", accountDesc = "Personal Saving Account", accountType = "Saving", accountBalance = 10000.879, customerId = 1 });
            lstAccount.Add(new Account() { accountId = 2, accountName = "HSBC Bank Account", accountDesc = "Personal Current Account", accountType = "Current", accountBalance = 1236712.6372, customerId = 2 });
            lstAccount.Add(new Account() { accountId = 3, accountName = "RBS Bank Account", accountDesc = "Personal Current Account", accountType = "Current", accountBalance = 7897923.00232, customerId = 1 });
            lstAccount.Add(new Account() { accountId = 4, accountName = "Chase Bank Account", accountDesc = "Personal Saving Account", accountType = "Saving", accountBalance = 5342009.00, customerId = 2 });
            lstAccount.Add(new Account() { accountId = 5, accountName = "Amex Account", accountDesc = "Personal Saving Account", accountType = "Saving", accountBalance = 821317.0783, customerId = 1 });

            return lstAccount;
        }
    }
}
