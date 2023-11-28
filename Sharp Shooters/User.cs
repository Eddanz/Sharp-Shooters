

namespace Sharp_Shooters
{
    internal class User
    {
        

        public string UserName {  get; set; }
        public int PinCode { get; set; }
        public List<Accounts> Accounts { get; set; }

        
        public User(string name, int pincode, List<Accounts> accounts)
        {
            UserName = name;
            PinCode = pincode;      
            Accounts = accounts;
        }

        public static void AccountOverview(User loggedInUser)
        {            
            Console.WriteLine($"The bankaccounts for: {loggedInUser.UserName.ToUpper()}:");
            int accountNumber = 0;
            foreach (var account in loggedInUser.Accounts)
            {
                accountNumber++;
                Console.WriteLine($"Account {accountNumber}: {account.AccountName}\nBalance: {account.AccountBalance}\n");                
            }            
        }
        
        public static User LogIn(List<User> users)
        {

            string enterName ="";
            int loginAttempts = 3; //The attempts the user has to login.
            while (loginAttempts != 0) //While-loop that runs as long as the login attempts are not 0.
            {
                Console.Clear();
                Console.Write("Username: ");
                enterName = Console.ReadLine().ToLower();
                Console.Write("Pincode: ");
                if (int.TryParse(Console.ReadLine(), out int enterPincode))
                {
                    //Using the FirstOfDeafult and Lambda expression it searches through the list of users and looks for a matching username and pincode, Return the the user as loggedInUser.
                    
                    User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.PinCode == enterPincode);

                    if (loggedInUser != null) //If loggedInUser has returned a value
                    {
                        Console.Clear();
                        Console.WriteLine($"\nLog in succesfull, Welcome {loggedInUser.UserName.ToUpper()}!" +
                            $"\nPlease wait while the information is retrived...");
                        Thread.Sleep(2000);
                        return loggedInUser; //Returns the loggedInUser
                        
                        
                    }
                    else //If the user enters the wrong credentials
                    {
                        loginAttempts--; //Remove one login attempt
                        Console.WriteLine($"\nWrong Credentials or you may have been blocked. \n If this problems continues, contact an administrator. \nYou have {loginAttempts} attempts left!\nPress enter to continue");
                        Console.ReadLine();
                    }
                }
                else //IF the user writes something else then numbers
                {
                    loginAttempts--; //Remove one login attempt
                    Console.WriteLine($"\nUnsuccesfull login. The Pincode can only contain numbers.\nYou have {loginAttempts} attempts left!\nPress enter to continue");
                    Console.ReadLine();
                }
            }
            if (loginAttempts == 0) //IF the login attempts reaches 0
            {
                User lockedUser = users.FirstOrDefault(a => a.UserName == enterName);//Search for the username in the list.
                Console.ForegroundColor = ConsoleColor.Red;//Change the text to red 
                Console.WriteLine("No more tries...");
                users.Remove(lockedUser);//Remove the user in then list so they cant login again.
                Console.WriteLine("The user is now locked. Contact an Admin to solve the issue..");
            }
            return null;
        }

        public static void OpenNewAccount(User user)//Method to open a new account
        {
            string currency = "";
            Console.Clear();
            Console.WriteLine("What currency is the account?\n [1] (£) Euro\n [2] ($) Dollar\n [3] (SEK) Kronor");
            string CAnswer = Console.ReadLine();
            switch (CAnswer)
            {
                case "1":
                    currency = "euro";
                    break;
                case "2":
                    currency = "dollar";
                    break;
                case "3":
                    currency = "kronor";
                    break;
                default:
                    Console.WriteLine("You must choose 1-3!");
                    break;
            }
            Console.Write("Name the account: ");
            string accountName = Console.ReadLine();

            Console.Write($"Deposit Cash in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency);
                user.Accounts.Add(newAccount);

                Console.WriteLine($"{accountName} has been created with a balance of {deposit:C}.");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }
        public static void OpenSavingsAccount(User user)//Method to open a savings account
        {
            Console.Clear();
            Console.Write("Name the account: ");
            string accountName = "Savings Account";
            double interest = 0.035;
            string currency = "";
            Console.Write($"Deposit Cashish in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit, currency);
                user.Accounts.Add(newAccount);

                Console.WriteLine($"{accountName} has been created with a balance of {deposit:C}.");
                Console.WriteLine($"The interest on your {accountName} will be: {deposit * interest:C}");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }
        public static void BorrowMoney(User user)
        {
            Console.Clear();
            List<User> LoanList = new List<User>();//This list regulates so the user can only loan once.
            // Calculate the combined balance of all accounts
            if (LoanList.Contains(user))//If the list contains the user it sends them back to the mainmenu.
            {
                Console.WriteLine("You have aldready made a loan! PAY IT OFF");
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadKey();
                
            }
            else
            {
                double combinedBalance = user.Accounts.Sum(account => account.AccountBalance);

                // Maximum amount cannot be greater than five times the users total balance.    
                double maxBorrowAmount = combinedBalance * 5;
                Console.WriteLine($"You can borrow up to {maxBorrowAmount:C}");
                Console.WriteLine("Due to an exceedingly high policy rate, the interest rate is currently at 5 percent");
                Console.Write("Enter the amount you want to borrow: ");
                if (double.TryParse(Console.ReadLine(), out double borrowAmount))
                {
                    if (borrowAmount <= 0)
                    {
                        Console.WriteLine("The amount must be greater than 0!");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return;
                    }

                    if (borrowAmount > maxBorrowAmount)
                    {
                        Console.WriteLine($"You cannot borrow more than five times the combined balance of all your accounts ({maxBorrowAmount:C}).");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("Select the account where you want to add the borrowed amount:");

                    int accountNumber = 0;
                    foreach (var account in user.Accounts)
                    {
                        accountNumber++;
                        Console.WriteLine($"{accountNumber}. {account.AccountName} (Balance: {account.AccountBalance:C})");
                    }

                    Console.Write("Enter the number of the account: ");
                    if (int.TryParse(Console.ReadLine(), out int accountChoice))
                    {
                        if (accountChoice >= 1 && accountChoice <= user.Accounts.Count)
                        {
                            user.Accounts[accountChoice - 1].AccountBalance += borrowAmount;
                            LoanList.Add(user);//Adds the user to the list so they cant loan more than once.
                            Console.WriteLine($"You have borrowed {borrowAmount:C}. The amount has been added to your {user.Accounts[accountChoice - 1].AccountName} Account.");
                            Console.WriteLine($"The interest rate on your loan will be {borrowAmount * 0.05}. Interest payments will begin next month.");
                            Console.WriteLine("Please press Enter to continue");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid account choice.");
                            Console.WriteLine("Please press Enter to continue");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for the account number.");
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for the borrowed amount. No money has been borrowed.");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                }
            }
        }
        //public static void TransactionHistory(User loggedInUser, List<string> transactionList) 
        //{
        //    Console.WriteLine($"Transaction history for {loggedInUser.UserName.ToUpper()}:");
        //    foreach (var transaction in transactionList)
        //    {
        //        Console.WriteLine(transaction);
        //    }
        //    Console.ReadLine();
        //}
        
        public static void Transfer(User loggedInUser, List<User> users)
        {
            List<string> transactionList = new List<string>();
            Console.WriteLine("[1] New transfer\n[2] Transfer history");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AccountOverview(loggedInUser);

                    Console.WriteLine("Which account do you want to transfer from?");
                    int.TryParse(Console.ReadLine(), out int fromAccountIndex);

                    if (fromAccountIndex < 1 || fromAccountIndex > loggedInUser.Accounts.Count)
                    {
                        Console.WriteLine("Choose a valid account number");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        return;
                    }

                    var sourceAccount = loggedInUser.Accounts[fromAccountIndex - 1];

                    Console.Clear();
                    Console.WriteLine($"Transfer from {sourceAccount.AccountName}\nBalance: {sourceAccount.AccountBalance}\n");
                    Console.WriteLine("Here are all the users in our system:");
                    foreach (var user in users)
                    {
                        Console.WriteLine(user.UserName.ToUpper());
                    }

                    Console.WriteLine("Enter the username of the recipient:");
                    string recipientUsername = Console.ReadLine().ToLower();

                    var recipientUser = users.FirstOrDefault(u => u.UserName == recipientUsername);

                    if (recipientUser == null)
                    {
                        Console.WriteLine("Recipient not found. Please enter a valid username.");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        return;
                    }

                    AccountOverview(recipientUser);

                    Console.WriteLine("Which account do you want to transfer to?");
                    int.TryParse(Console.ReadLine(), out int toAccountIndex);

                    if (toAccountIndex < 1 || toAccountIndex > recipientUser.Accounts.Count)
                    {
                        Console.WriteLine("Choose a valid account number for the recipient");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                        return;
                    }

                    var destinationAccount = recipientUser.Accounts[toAccountIndex - 1];

                    Console.WriteLine($"Transfer to {destinationAccount.AccountName}\nBalance: {destinationAccount.AccountBalance}");

                    Console.WriteLine("Enter the amount to transfer:");
                    if (int.TryParse(Console.ReadLine(), out int amount))
                    {
                        if (amount > 0 && amount <= sourceAccount.AccountBalance)
                        {
                            sourceAccount.AccountBalance -= amount;
                            destinationAccount.AccountBalance += amount;

                            DateTime date = DateTime.Now;
                            Console.WriteLine($"Transfer of {amount} to {recipientUser.UserName}'s {destinationAccount.AccountName} successful!");
                            string transaction = $"Transfer of {amount} to {recipientUser.UserName}'s {destinationAccount.AccountName} Date: {date}";
                            transactionList.Add(transaction);
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Make sure it's a positive value and within your account balance.");
                            Console.WriteLine("Press Enter to continue");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadKey();
                    }
                    break;
                case "2":
                    Console.WriteLine($"Transaction history for {loggedInUser.UserName.ToUpper()}:");
                    //foreach (var transaction in transactionList.)
                    for (int i = 0; i < transactionList.Count; i++)
                    {
                        Console.WriteLine(i);
                        Console.WriteLine("TEST");

                    }
                    Console.ReadLine();

                    break;
            }
            
            
        }
        //public class Scheduler(List<string> transactionList)
        //{
        //    private Timer timer;
            

        //    public Scheduler()
        //    {
        //        // Set up a timer to execute the transactions every 15 minutes
        //        timer = new Timer(ExecuteScheduledTransactions, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        //        pendingTransactions = new List<transactionList>();
        //    }

        //    public void Schedule(transactionList transaction)
        //    {
        //        // Add the transaction to the list of pending transactions
        //        pendingTransactions.Add(transaction);
        //        Console.WriteLine("Transaction scheduled.");
        //    }

        //    private void ExecuteScheduledTransactions(object state)
        //    {
        //        // Execute pending transactions
        //        foreach (var transaction in pendingTransactions)
        //        {
        //            // Execute the transaction
        //            Console.WriteLine("Executing transaction...");
        //            // Your logic to execute the transaction goes here
        //        }

        //        // Clear the list after executing all transactions
        //        pendingTransactions.Clear();
        //    }
        //}


    }
    
}
