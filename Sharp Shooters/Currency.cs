
namespace Sharp_Shooters
{ 
    internal static class Currency //Here we declare the exchange rates for all of the conversions
    {
        private static double _usdEurCur = 0.93;
        private static double _usdKronorCur = 10.86;
        private static double _eurUsdCur = 1.07;
        private static double _eurKronorCur = 11.68;
        private static double _kronorUsdCur = 0.1;
        private static double _kronorEurCur = 0.091;
        public static List<User> LoanList = new List<User>(); //This list regulates so the user can only make a loan once.

        public static double USDEURCUR //USD Dollar to Euro
        {
            get
            {
                return _usdEurCur;
            }
            set
            {
                _usdEurCur = value;
            }
        }
        public static double USDKRONORCUR //USD Dollar to Kronor
        {
            get
            {
               return _usdKronorCur;
            }
            set
            {

                _usdKronorCur = value;
            }
        }
        public static double EURUSDCUR //Euro to USD Dollar
        {
            get
            {
                return _eurUsdCur;
            }
            set
            {

                _eurUsdCur = value;
            }
        }
        public static double EURKRONORCUR // Euro to Kronor
        {
            get
            {
                return _eurKronorCur;
            }
            set
            {

                _eurKronorCur = value;
            }
        }
        public static double KRONORUSDCUR //Kronor to USD Dollar
        {
            get
            {
                return _kronorUsdCur;
            }
            set
            {

                _kronorUsdCur = value;
            }
        }
        public static double KRONOREURCUR //Kronor to Euro
        {
            get
            {
                return _kronorEurCur;
            }
            set
            {
                
                _kronorEurCur = value;
            }
        }

        public static double GetExchangeRate(Accounts fromCurrency, Accounts toCurrency) //This method looks at what the currency of the account the user wants to send money from is and the at what the account the user want to send money to is.
        {
            
            double returnCurrency = 1.0;
            switch (fromCurrency.Currencies)
            {
                
                case "USD":
                    switch (toCurrency.Currencies)
                    {
                        case "EURO":
                            returnCurrency = USDEURCUR;
                            return returnCurrency;
                        case "KRONOR":
                            returnCurrency = USDKRONORCUR;
                            return returnCurrency;
                    }
                    break;
                case "EURO":
                    switch (toCurrency.Currencies)
                    {
                        case "USD":
                            returnCurrency = EURUSDCUR;
                            return returnCurrency;
                        case "KRONOR":
                            returnCurrency = EURKRONORCUR;
                            return returnCurrency;
                    }
                    break;
                case "KRONOR":
                    switch (toCurrency.Currencies)
                    {
                        case "USD":
                            returnCurrency = KRONORUSDCUR;
                            return returnCurrency;
                        case "EURO":
                            returnCurrency = KRONOREURCUR;
                            return returnCurrency;
                    }
                    break;
                default: 
                    return returnCurrency;
                    
            }
            return returnCurrency;
        }
        public static double ConvertCurrency(double amount, Accounts fromCurrency, Accounts toCurrency) // this method converts the currencies of the different accounts by using the method "GetExchangeRate"
        {
            if (fromCurrency.Currencies == toCurrency.Currencies)
            {
                // Currencies are the same, no need to convert
                return amount;
            }
            else
            {
                // Currencies are different, perform conversion
                double exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
                double convertedAmount = amount * exchangeRate;
                return convertedAmount;
            }
        }

        public static double ConvertCurrency(List<Accounts> accounts, Accounts toCurrency)
        {
            double totalAmount = 0;

            foreach (var account in accounts) 
            {
                if (account.Currencies != toCurrency.Currencies)
                {
                    double exchangeRate = GetExchangeRate(account, toCurrency);
                    double convertedAmount = account.AccountBalance * exchangeRate;
                    totalAmount += convertedAmount;
                }
                else
                {
                    totalAmount += account.AccountBalance;
                }
            }

            return totalAmount;
        }

        public static void UpdateCurrency() //The Admin can update the currencies to the daily rates.
        {
            Console.WriteLine("Welcome to the Currency updater 2.0" +
                "\n\n[1] START" +
                "\n[2] Back to Menu");
            var MenuChoice = Console.ReadLine();
            switch (MenuChoice)
            {
                case "1":
                    try
                    {
                        Console.WriteLine("Update USD to EURO: ");
                        USDEURCUR = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Update USD to KRONOR: ");
                        USDKRONORCUR = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Update EURO to USD: ");
                        EURUSDCUR = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Update EURO to KRONOR: ");
                        EURKRONORCUR = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Update KRONOR to USD: ");
                        KRONORUSDCUR = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Update KRONOR to EURO: ");
                        KRONOREURCUR = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("The value must be in numbers");
                    }
                    break;
                case "2":
                    
                    return;
            }
        }

        public static void BorrowMoney(User loggedInUser, List<Accounts> accounts) //This method lets the take out a loan one time. The user can take a loan of maximum five times the value of all of their accounts.
        {
            Accounts SEK = new Accounts("SEK", 0, "KRONOR", "SEK");
            Console.Clear();
            // Calculate the combined balance of all accounts
            if (LoanList.Contains(loggedInUser))//If the list contains the user it sends them back to the mainmenu.
            {
                Console.WriteLine("\nYou have already made a loan! PAY IT OFF");
                Utility.UniqueReadKeyMethod();

            }
            else
            {
                double combinedBalance = ConvertCurrency(accounts, SEK);
                    //user.Accounts.Sum(account => account.AccountBalance);

                // Maximum amount cannot be greater than five times the users total balance.    
                double maxBorrowAmount = combinedBalance * 5;
                Console.WriteLine($"\nYou can borrow up to {maxBorrowAmount:C}" +
                "\nDue to an exceedingly high policy rate, the interest rate is currently at 5 percent" +
                "\nEnter the amount you want to borrow: ");
                if (double.TryParse(Console.ReadLine(), out double borrowAmount))
                {
                    if (borrowAmount <= 0) //Error handling
                    {
                        Console.WriteLine("The amount must be greater than 0!");
                        Utility.UniqueReadKeyMethod();
                    }
                    if (borrowAmount > maxBorrowAmount)//Error handling
                    {
                        Console.WriteLine($"You cannot borrow more than five times the combined balance of all your accounts ({maxBorrowAmount:C}).");
                        Utility.UniqueReadKeyMethod();
                        return;
                    }
                    Accounts loan = new Accounts("Loan", borrowAmount, "KRONOR", "SEK");
                    loggedInUser.Accounts.Add(loan);
                    LoanList.Add(loggedInUser);//Adds the user to the list so they cant loan more than once.
                    Console.Clear();
                    Console.WriteLine($"\nYou have borrowed {borrowAmount:C}. The amount has been added to your new loan account." +
                    $"\nThe interest rate on your loan will be {borrowAmount * 0.05:C}. Interest payments will begin next month.");
                    Utility.UniqueReadKeyMethod();
                   
                }
                else
                {
                    Console.WriteLine("Invalid input for the borrowed amount. No money has been borrowed.");//Error handling
                    Utility.UniqueReadKeyMethod();
                }
            }
        }
    }
}
