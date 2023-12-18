
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
        public static List<LoanTransaction> loanTransactions = new List<LoanTransaction>();

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

        private static double GetExchangeRate(Accounts fromCurrency, Accounts toCurrency) //This method looks at what the currency of the account the user wants to send money from is and the at what the account the user want to send money to is.
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

        private static double ConvertCurrency(List<Accounts> accounts, Accounts toCurrency)
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

            return Math.Round(totalAmount, 1);
        }

        private static double maxBorrowMultiplier = 5;
        private static double interestRate = 0.05;

        public static void BorrowMoney(User loggedInUser, List<Accounts> accounts)
        {
            
            double initialTotalBalance = loggedInUser.InitialTotalBalance; // initial value is always 0 when we start the program

            if (initialTotalBalance == 0)
            {
                // Calculate the initial total balance only if it hasn't been calculated yet
                initialTotalBalance = ConvertCurrency(accounts, new Accounts("SEK", 0, "KRONOR", "SEK"));
                loggedInUser.InitialTotalBalance = initialTotalBalance; // assigns the new value so that the program wont run this function again for the specific user 
            }

            // Calculate the maximum amount the user can borrow initially (5 times the initial total balance)
            double maxInitialBorrowAmount = initialTotalBalance * maxBorrowMultiplier;

            // Calculate the total amount the user has already borrowed
            double totalBorrowedAmount = CalculateTotalBorrowedAmount(loanTransactions);

            // Calculate the remaining amount the user can borrow
            double remainingBorrowLimit = maxInitialBorrowAmount - totalBorrowedAmount;

            if (remainingBorrowLimit <= 0)
            {
                Console.WriteLine("\nYou have reached the maximum borrowing limit, PAY IT OFF!");
                Utility.UniqueReadKeyMethod();
                return;
            }
            else
            {
                Console.Write($"\nYou can borrow up to {remainingBorrowLimit:C} (No currency exchange fees)" +
                $"\nThe loan period is 10 years, Due to an exceedingly high policy rate, the interest rate is currently at {interestRate:P}" +
                "\nEnter the amount you want to borrow: ");

                if (TryGetBorrowAmount(out double borrowAmount) && IsValidBorrowAmount(borrowAmount, remainingBorrowLimit))
                {
                    // Process the loan
                    Accounts findAccount = loggedInUser.Accounts.FirstOrDefault(a => a.AccountName == "Loan");
                    if (findAccount == null) // If the user does not have a Loan account, create Loan account.
                    {
                        Accounts loan = new Accounts("Loan", borrowAmount, "KRONOR", "SEK");
                        loggedInUser.Accounts.Add(loan);
                        LoanTransaction loanTransaction = new LoanTransaction(borrowAmount);
                        loanTransactions.Add(loanTransaction);
                    }
                    else // Adds the balance to existing Loan account.
                    {
                        findAccount.AccountBalance += borrowAmount;
                        LoanTransaction loanTransaction = new LoanTransaction(borrowAmount);
                        loanTransactions.Add(loanTransaction);
                    }
                    
                    Console.Clear();
                    //Adds the user to the list so they cant loan more than once.
                    Console.WriteLine($"\nYou have borrowed {borrowAmount:C}. The amount has been added to your new loan account." +
                        $"\nThe interest rate on your loan will be {borrowAmount * interestRate:C}. Interest payments will begin next month.");
                    Utility.UniqueReadKeyMethod();
                }
                else
                {
                    Console.WriteLine("Invalid input for the borrowed amount. No money has been borrowed.");
                    Utility.UniqueReadKeyMethod();
                }
            }
        }

        private static double CalculateTotalBorrowedAmount(List<LoanTransaction> loanTransactions)
        {
            //Calculate the total amount the user has already borrowed
            double totalBorrowedAmount = loanTransactions.Sum(transaction => transaction.Amount);
            return totalBorrowedAmount;
        }

        private static bool TryGetBorrowAmount(out double borrowAmount)
        {
            if (double.TryParse(Console.ReadLine(), out borrowAmount))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid input for the borrowed amount.");
                return false;
            }
        }

        private static bool IsValidBorrowAmount(double borrowAmount, double remainingBorrowLimit)
        {
            if (borrowAmount <= 0)
            {
                Console.WriteLine("The amount must be greater than 0!");
                Utility.UniqueReadKeyMethod();
                return false;
            }

            if (borrowAmount > remainingBorrowLimit)
            {
                Console.WriteLine($"You cannot borrow more than the remaining limit ({remainingBorrowLimit:C}).");
                Utility.UniqueReadKeyMethod();
                return false;
            }

            return true;
        }
        public static void LoanPayment()
        {
            double totalBorrowedAmount = CalculateTotalBorrowedAmount(loanTransactions); //Caclute the total borrowed amount
            double monthlyInterestPayment = totalBorrowedAmount * 0.05 / 12; //Calculate the intreset on the loan
            double monthlyAmortizationPayment = totalBorrowedAmount / 120; //Calculate the amorization

            Console.WriteLine($"You have loaned {totalBorrowedAmount:C}" +
            $"\nThe next payment for your intrest exchange is: {monthlyInterestPayment:C}" +
            $"\nThe amortization for your loan is: {monthlyAmortizationPayment:C}");
            Utility.UniqueReadKeyMethod();
        }
    }
}
