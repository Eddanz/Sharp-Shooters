
namespace Sharp_Shooters
{
    internal class Accounts //We declare what a account is. An Account needs to have the properties listed below.
    {
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
        public string Currencys {  get; set; }
        public string Sign { get; set; }

        public Accounts(string accountName, double accountBalance, string currency, string sign) //Constructor for the accounts.
        {
            AccountName = accountName;
            AccountBalance = accountBalance;
            Currencys = currency;
            Sign = sign;
        }

        public static void AccountOverview(User loggedInUser) //The method that shows what accounts that is avaliable for the logged in user.
        {
            Console.Clear();
            Console.WriteLine($"\nThe bankaccounts for {loggedInUser.UserName.ToUpper()}:\n");
            int accountNumber = 0;
            foreach (var account in loggedInUser.Accounts) //Foreach loop that loops through the list of accounts for the user.
            {
                accountNumber++;
                Console.WriteLine($"Account {accountNumber}: {account.AccountName}\nBalance: {account.AccountBalance} {account.Sign}\n");
            }
        }
        public static void OpenNewAccount(User loggedInUser)//Method to open a new account.
        {
            string sign = "";
            string currency = "";

            Console.Write("Name the account: "); //The user can name the account
            string accountName = Console.ReadLine();

            CurrencySelectionMenu(out currency, out sign);

            Console.Write($"Deposit Cash in {accountName}: "); //The user deposits cash to the newly created account
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency, sign);
                loggedInUser.Accounts.Add(newAccount);

                Console.WriteLine($"\n{accountName} has been created with a balance of {sign} {deposit}.");
                Utility.UniqueReadKeyMeth();
            }
        }
        public static void OpenSavingsAccount(User loggedInUser)//Method to open a savings account
        {
            string accountName = "Savings Account";
            double interest = 0.035; //We set the savings rate to 3.5%
            string currency = "";
            string sign = "";

            CurrencySelectionMenu(out currency, out sign);

            Console.Write($"Deposit Cash in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency, sign);
                loggedInUser.Accounts.Add(newAccount);

                Console.WriteLine($"\n{accountName} has been created with a balance of {sign} {deposit}");
                Console.WriteLine($"The interest on your {accountName} will be {sign}: {deposit * interest:F2}");
                Utility.UniqueReadKeyMeth();
            }
        }
        public static void CurrencySelectionMenu(out string currency, out string sign)
        {
            currency = "";
            sign = "";
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
                        sign = "£";
                        validCurrency = true;
                        break;
                    case "2":
                        currency = "USD";
                        sign = "$";
                        validCurrency = true;
                        break;
                    case "3":
                        currency = "KRONOR";
                        sign = "SEK";
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
