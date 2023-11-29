
namespace Sharp_Shooters
{
    internal class TransferData //The properties we need to make a transfer
    {
        public User LoggedInUser { get; }
        public Accounts SourceAccount { get; }
        public User RecipientUser { get; }
        public Accounts DestinationAccount { get; }
        public double Amount { get; }

        public TransferData(User loggedInUser, Accounts sourceAccount, User recipientUser, Accounts destinationAccount, double amount)
        {
            LoggedInUser = loggedInUser;
            SourceAccount = sourceAccount;
            RecipientUser = recipientUser;
            DestinationAccount = destinationAccount;
            Amount = amount;
        }

        public static void Transfer(User loggedInUser, List<User> users) //This method does the transfers.
        {

            Console.WriteLine("[1] New transfer\n[2] Transfer history");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":                    
                    Accounts.AccountOverview(loggedInUser); //When we initialize a new transfer we first list the avaliable accounts for the logged in user.

                    Console.WriteLine("Which account do you want to transfer from?");
                    int.TryParse(Console.ReadLine(), out int fromAccountIndex);

                    if (fromAccountIndex < 1 || fromAccountIndex > loggedInUser.Accounts.Count) // If the user chooses a number that does not have an account we send them back to the main menu.
                    {
                        Utility.UniversalReadKeyMeth();
                        return;
                    }

                    var sourceAccount = loggedInUser.Accounts[fromAccountIndex - 1]; //Use - 1 of the index because the index starts at 0 but we present it from 1.

                    Console.Clear();
                    Console.WriteLine($"Transfer from {sourceAccount.AccountName}\nBalance: {sourceAccount.AccountBalance}\n");
                    Console.WriteLine("Here are all the users in our system:");
                    foreach (var user in users) //List all of the users in the system.
                    {
                        Console.WriteLine(user.UserName.ToUpper());
                    }

                    Console.WriteLine("\nEnter the username of the recipient:");
                    string recipientUsername = Console.ReadLine().ToLower();

                    var recipientUser = users.FirstOrDefault(u => u.UserName == recipientUsername); //FirstOrDefault and lambda to search for the user.

                    if (recipientUser == null) //If the user does not exist, send back to main menu.
                    {
                        Utility.UniversalReadKeyMeth();
                        return;
                    }

                    Accounts.AccountOverview(recipientUser); //Shows all of the accounts of the recipient user.

                    Console.WriteLine("Which account do you want to transfer to?");
                    int.TryParse(Console.ReadLine(), out int toAccountIndex);

                    if (toAccountIndex < 1 || toAccountIndex > recipientUser.Accounts.Count)
                    {
                        Utility.UniversalReadKeyMeth();
                        return;
                    }

                    var destinationAccount = recipientUser.Accounts[toAccountIndex - 1];
                    
                    Console.WriteLine($"\nTransfer to {destinationAccount.AccountName}\nBalance: {destinationAccount.AccountBalance}");

                    Console.WriteLine("Enter the amount to transfer:");
                    if (double.TryParse(Console.ReadLine(), out double amount))
                    {
                        if (amount > 0 && amount <= sourceAccount.AccountBalance)
                        {
                            Timer transferTimer = new Timer(TransferCallback, new TransferData(loggedInUser, sourceAccount, recipientUser, destinationAccount, amount), 1 * 60 * 1000, Timeout.Infinite); //The transaction is scheduled 15 minutes forward in time.
                            Console.WriteLine($"\nTransfer of {amount} {sourceAccount.Sign} to {recipientUser.UserName}'s {destinationAccount.AccountName} scheduled in 15 minutes.\nPress Enter to continue");
                            Console.ReadKey();
                        }
                        else
                        {
                            Utility.UniversalReadKeyMeth();
                        }
                    }
                    else
                    {
                        Utility.UniversalReadKeyMeth();
                    }
                    break;
                case "2":
                    TransactionHistory(loggedInUser);
                    //Console.WriteLine($"Transaction history for {loggedInUser.UserName.ToUpper()}:"); // Present all of the transactions for the logged in user.
                    //foreach (var transaction in loggedInUser.Transactions)
                    //{
                    //    Console.WriteLine(transaction);
                    //}
                    //Console.ReadLine();
                    break;
            }
        }
        private static void TransferCallback(object state)
        {
            if (state is TransferData transferData)
            {
                double convertedAmount = Currency.ConvertCurrency(transferData.Amount, transferData.SourceAccount, transferData.DestinationAccount);
                transferData.SourceAccount.AccountBalance -= transferData.Amount;
                transferData.DestinationAccount.AccountBalance += convertedAmount;

                DateTime date = DateTime.Now;
                string transaction = $"Transfer of {transferData.Amount} to {transferData.RecipientUser.UserName}'s {transferData.DestinationAccount.AccountName} Date: {date}";
                transferData.LoggedInUser.Transactions.Add(transaction);
            }
        }

        private static void TransactionHistory(User loggedInUser)
        {
            Console.WriteLine($"Transaction history for {loggedInUser.UserName.ToUpper()}:"); // Present all of the transactions for the logged in user.
            foreach (var transaction in loggedInUser.Transactions)
            {
                Console.WriteLine(transaction);
            }
            Console.ReadLine();            
        }
    }
}
