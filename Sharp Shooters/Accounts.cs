using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Shooters
{
    internal class Accounts
    {
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
        public string Currency {  get; set; }

        public Accounts(string accountName, double accountBalance, string currency)
        {
            AccountName = accountName;
            AccountBalance = accountBalance;
            Currency = currency;
        }
        
    }
}
