
namespace Sharp_Shooters
{
    internal class Accounts //We declare what a account is. An Account needs to have the properties listed below.
    {
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
        public string Currencies {  get; set; }
        public string CurrencySymbol { get; set; }

        public Accounts(string accountName, double accountBalance, string currency, string currencySymbol) //Constructor for the accounts.
        {
            AccountName = accountName;
            AccountBalance = accountBalance;
            Currencies = currency;
            CurrencySymbol = currencySymbol;
        }

        public static void AccountOverview(User loggedInUser) //The method that shows what accounts that is avaliable for the logged in user.
        {
            Console.Clear();
            Console.WriteLine($"\nThe bank accounts for {loggedInUser.UserName.ToUpper()}:\n");
            int accountNumber = 0;
            foreach (var account in loggedInUser.Accounts) //Foreach loop that loops through the list of accounts for the user.
            {
                accountNumber++;
                Console.WriteLine($"Account {accountNumber}: {account.AccountName}\nBalance: {Math.Round(account.AccountBalance, 2)} {account.CurrencySymbol}\n");
            }
        }
        public static void OpenNewAccount(User loggedInUser)//Method to open a new account. 
        {
            string currencySymbol = "";
            string currency = "";

            Console.Write("Name the account: "); //The user can name the account
            string accountName = Console.ReadLine();

            CurrencySelectionMenu(out currency, out currencySymbol);

            Console.Write($"Deposit Cash in {accountName}: "); //The user deposits cash to the newly created account
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency, currencySymbol);
                loggedInUser.Accounts.Add(newAccount);

                Console.WriteLine($"\n{accountName} has been created with a balance of {currencySymbol} {deposit}.");
                Utility.UniqueReadKeyMethod();
            }
        }

        public static void DeleteAccount(User loggedInUser)
        {
            // Create a list of accounts with a balance of 0
            var accountsWithZeroBalance = loggedInUser.Accounts.Where(account => account.AccountBalance == 0).ToList();
            Console.Clear();
            int accountNumber = 0;
            if (accountsWithZeroBalance.Count == 0)
            {
                Console.WriteLine("\nYou can only remove accounts with the balance 0, none found.");
                Utility.UniqueReadKeyMethod();
            }
            else 
            {
                Console.WriteLine("\nAccounts with the balance of 0:");
                foreach (var account in accountsWithZeroBalance)
                {
                    accountNumber++;
                    Console.WriteLine($"\nAccount {accountNumber}: {account.AccountName}");
                }

                Console.WriteLine("\nWhich account do you want to remove?");
                int.TryParse(Console.ReadLine(), out int RemoveAccount);

                if (RemoveAccount < 1 || RemoveAccount > accountsWithZeroBalance.Count) // If the user chooses an invalid account number, return to the main menu.
                {
                    Utility.UniversalReadKeyMethod();
                    return;
                }

                var deleteAccount = accountsWithZeroBalance[RemoveAccount - 1];
                loggedInUser.Accounts.Remove(deleteAccount);
                Console.WriteLine($"\nYou have removed {deleteAccount.AccountName}");
                Utility.UniqueReadKeyMethod();
            }
        }

        public static void OpenSavingsAccount(User loggedInUser)//Method to open a savings account
        {
            string accountName = "Savings Account";
            double interest = 0.035; //We set the savings rate to 3.5%
            string currency = "";
            string currencySymbol = "";

            CurrencySelectionMenu(out currency, out currencySymbol);

            Console.Write($"Deposit Cash in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency, currencySymbol);
                loggedInUser.Accounts.Add(newAccount);

                Console.WriteLine($"\n{accountName} has been created with a balance of {currencySymbol} {deposit}");
                Console.WriteLine($"The interest on your {accountName} will be {currencySymbol}: {deposit * interest:F2}");
                Utility.UniqueReadKeyMethod();
            }
        }
        private static void CurrencySelectionMenu(out string currency, out string currencySymbol)
        {
            currency = "";
            currencySymbol = "";
            bool validCurrency = false; //Ensures that the user makes a valid choice
            while (!validCurrency) // Will loop the switch case until criteria is met
            {
                Console.Clear();
                Console.WriteLine("\nWhat currency is the account?\n [1] (£) Euro\n [2] ($) Dollar\n [3] (SEK) Kronor");//The user can choose what currency the account should hold.
                Console.Write("\nSelect between 1-3: ");
                string currencySelection = Console.ReadLine();
                switch (currencySelection)
                {
                    case "1":
                        currency = "EURO";
                        currencySymbol = "£";
                        validCurrency = true;
                        break;
                    case "2":
                        currency = "USD";
                        currencySymbol = "$";
                        validCurrency = true;
                        break;
                    case "3":
                        currency = "KRONOR";
                        currencySymbol = "SEK";
                        validCurrency = true;
                        break;
                    default:
                        Console.WriteLine("You must choose 1-3!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
    }
}
