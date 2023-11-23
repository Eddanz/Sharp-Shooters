

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
                Console.WriteLine($"Account {accountNumber}: {account.AccountName}\nBalance: {account.AccountBalance}");
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
                User lockedUser = users.FirstOrDefault(u => u.UserName == enterName);//Search for the username in the list.
                Console.ForegroundColor = ConsoleColor.Red;//Change the text to red 
                Console.WriteLine("No more tries...");
                users.Remove(lockedUser);//Remove the user in then list so they cant login again.
                Console.WriteLine("The user is now locked. Contact an Admin to solve the issue..");
            }
            return null;
        }
        public static void OpenNewAccount(User user)//Method to open a new account
        {
            Console.Clear();
            Console.Write("Name the account: ");
            string accountName = Console.ReadLine();

            Console.Write($"Deposit Cashish in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit);
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

            Console.Write($"Deposit Cashish in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit);
                user.Accounts.Add(newAccount);

                Console.WriteLine($"{accountName} has been created with a balance of {deposit:C}.");
                Console.WriteLine($"The interest on your {accountName} will be: {deposit * interest:C}");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }

        public static void Transfer(User loggedInUser, List<User> users)
        {
            AccountOverview(loggedInUser);

            Console.WriteLine("Which account do you want to transfer from?");
            int.TryParse(Console.ReadLine(), out int fromAccountIndex);

            if (fromAccountIndex < 1 || fromAccountIndex > loggedInUser.Accounts.Count)
            {
                Console.WriteLine("Choose a valid account number");
                Console.ReadKey();
                Console.WriteLine("Press Enter to continue");
                return;
            }

            var sourceAccount = loggedInUser.Accounts[fromAccountIndex - 1];

            Console.WriteLine($"Transfer from {sourceAccount.AccountName}\nBalance: {sourceAccount.AccountBalance}");
            
            Console.WriteLine("Enter the username of the recipient:");//Lägg in alla användare
            string recipientUsername = Console.ReadLine().ToLower();

            var recipientUser = users.FirstOrDefault(u => u.UserName == recipientUsername);

            if (recipientUser == null)
            {
                Console.WriteLine("Recipient not found. Please enter a valid username.");
                Console.ReadKey();
                Console.WriteLine("Press Enter to continue");
                return;
            }

            AccountOverview(recipientUser);

            Console.WriteLine("Which account do you want to transfer to?");
            int.TryParse(Console.ReadLine(), out int toAccountIndex);

            if (toAccountIndex < 1 || toAccountIndex > recipientUser.Accounts.Count)
            {
                Console.WriteLine("Choose a valid account number for the recipient");
                Console.ReadKey();
                Console.WriteLine("Press Enter to continue");
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

                    Console.WriteLine($"Transfer of {amount} to {recipientUser.UserName}'s {destinationAccount.AccountName} successful!");
                    Console.ReadKey();
                    Console.WriteLine("Press Enter to continue");
                }
                else
                {
                    Console.WriteLine("Invalid amount. Make sure it's a positive value and within your account balance.");
                    Console.ReadKey();
                    Console.WriteLine("Press Enter to continue");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric amount.");
                Console.ReadKey();
                Console.WriteLine("Press Enter to continue");
            }
        }

        public static void LoanMoney(List<User> users)
        {
          
        }
    }
    
}
