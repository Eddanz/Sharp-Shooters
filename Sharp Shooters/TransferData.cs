
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

            Console.WriteLine("\n[1] New transfer\n[2] Transfer history");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    NewTransfer(loggedInUser, users);
                    break;
                case "2":
                    TransactionHistory(loggedInUser);
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
                string transaction = $"Transfer of {transferData.Amount} {transferData.SourceAccount.CurrencySymbol} to {transferData.RecipientUser.UserName.ToUpper()}'s {transferData.DestinationAccount.AccountName} Date: {date}";
                string recipient = $"You received {transferData.Amount} {transferData.SourceAccount.CurrencySymbol} to your {transferData.DestinationAccount.AccountName} from {transferData.LoggedInUser.UserName.ToUpper()} Date: {date}";
                transferData.LoggedInUser.Transactions.Add(transaction);
                if (transferData.LoggedInUser.UserName != transferData.RecipientUser.UserName)
                {
                    transferData.RecipientUser.Transactions.Add(recipient);
                }
                
            }
        }

        private static void NewTransfer(User loggedInUser, List<User> users)
        {
            Accounts.AccountOverview(loggedInUser); //When we initialize a new transfer we first list the available accounts for the logged in user.

            Console.WriteLine("Which account do you want to transfer from?");
            int.TryParse(Console.ReadLine(), out int fromAccountIndex);

            if (fromAccountIndex < 1 || fromAccountIndex > loggedInUser.Accounts.Count) // If the user chooses a number that does not have an account we send them back to the main menu.
            {
                Utility.UniversalReadKeyMethod();
                return;
            }

            var sourceAccount = loggedInUser.Accounts[fromAccountIndex - 1]; //Use - 1 of the index because the index starts at 0 but we present it from 1.

            Console.Clear();
            Console.WriteLine($"\nTransfer from {sourceAccount.AccountName}\nBalance: {sourceAccount.AccountBalance} {sourceAccount.CurrencySymbol}\n" +
            "\nHere are all the users in our system:");
            foreach (var user in users) //List all of the users in the system.
            {
                Console.WriteLine(user.UserName.ToUpper());
            }

            Console.WriteLine("\nEnter the username of the recipient:");
            string recipientUsername = Console.ReadLine().ToLower();

            var recipientUser = users.FirstOrDefault(u => u.UserName == recipientUsername); //FirstOrDefault and lambda to search for the user.

            if (recipientUser == null) //If the user does not exist, send back to main menu.
            {
                Utility.UniversalReadKeyMethod();
                return;
            }

            Accounts.AccountOverview(recipientUser); //Shows all of the accounts of the recipient user.

            Console.WriteLine("Which account do you want to transfer to?");
            int.TryParse(Console.ReadLine(), out int toAccountIndex);

            if (toAccountIndex < 1 || toAccountIndex > recipientUser.Accounts.Count)
            {
                Utility.UniversalReadKeyMethod();
                return;
            }

            var destinationAccount = recipientUser.Accounts[toAccountIndex - 1];

            Console.WriteLine($"\nTransfer to {recipientUser.UserName.ToUpper()}'s {destinationAccount.AccountName}\nBalance on your account {sourceAccount.AccountName} {sourceAccount.AccountBalance} {sourceAccount.CurrencySymbol}");

            Console.Write("Enter the amount to transfer: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (amount > 0 && amount <= sourceAccount.AccountBalance)
                {
                    Timer transferTimer = new Timer(TransferCallback, new TransferData(loggedInUser, sourceAccount, recipientUser, destinationAccount, amount),  1 * 1000, Timeout.Infinite); //The transaction is scheduled 15 minutes forward in time.
                    Console.WriteLine($"\nTransfer of {amount} {sourceAccount.CurrencySymbol} to {recipientUser.UserName}'s {destinationAccount.AccountName} scheduled in 15 minutes.\nPress Enter to continue");
                    Console.ReadKey();
                }
                else
                {
                    Utility.UniversalReadKeyMethod();
                }
            }
            else
            {
                Utility.UniversalReadKeyMethod();
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
