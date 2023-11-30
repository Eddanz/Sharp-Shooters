
namespace Sharp_Shooters
{
    internal class Bank
    {                                   //Main menu with all the optins the user can make.
        public static void WelcomeMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n         Welcome to SharpShooter Bank" +
            "\n         ----------------------------" +
            "\n                 _ _.-'`-._ _\r\n                ;.'________'.;\r\n     _________n.[____________].n_________\r\n    |\"\"_\"\"_\"\"_\"\"||==||==||==||\"\"_\"\"_\"\"_\"\"]\r\n    |\"\"\"\"\"\"\"\"\"\"\"||..||..||..||\"\"\"\"\"\"\"\"\"\"\"|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n    |.. .. .. ..||..||..||..||.. .. .. ..|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n ,,;;,;;;,;;;,;;;,;;;,;;;,;;;,;;,;;;,;;;,;;,,\r\n;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
            DailyMessage();
            Console.WriteLine("\nPress Enter to Log in!");
            Console.ReadKey();
        }

        public static void DailyMessage() 
        { 
            Random random = new Random();
            List<string> messages = new List<string>
            {
                "\nThe only bank you need in your life!",
                "\nExpertise you need. Service you deserve!",
                "\nWe Built This Bank For You!",
                "\nYour First Choice!",
                "\nWe know money!",
                "\nBanking for people with better things to do!",
                "\nBecause life’s complicated enough!",
                "\nMake Dreams Happen!",
                "\nYour First Choice!",
                "\nBanking Focused on You!"
            };
            string randomMessage = messages[random.Next(messages.Count)];
            Console.WriteLine(randomMessage);
        }

        public static void MainMenu(User loggedInUser, List<User> users) //Overloading the method with the loggedin user and the list of all of the users created.
        {
            while (true) //While-loop so it loops back after the user is done with its action 
            {
                Console.Clear();
                Console.Write($"\n===== You are logged in as: {loggedInUser.UserName.ToUpper()} =====" +
                    $"\n\nMake a choise below" +
                    $"\n[1] Manage accounts" +
                    $"\n[2] Set up a loan" +
                    $"\n[3] Transfer" +
                    $"\n[4] Log out" +
                    $"\n\nCHOISE: ");
                string userChoise = Console.ReadLine();
                switch (userChoise)
                {
                    case "1": //Use the Account overview method to display all the accounts the user has.
                        bool valid = false;
                        while (!valid)
                        {
                            Console.Clear();
                            Accounts.AccountOverview(loggedInUser);
                            Console.WriteLine("[1] Open a new account" +
                                 "\n[2] Go back to main menu"); //If the user presses "2" they can open a new account using either the "OpenNewAccount" or "OpenSavingsAccount" method.
                            string userChoise1 = Console.ReadLine();
                            switch (userChoise1)
                            {
                                case "1": // The user can choose between opening a regular account or a savings account that has a rate of 3.5%.
                                    Console.Clear();
                                    valid = true;
                                    Console.WriteLine("\n[1] Open a regular account" +
                                        "\n[2] Open a savings account");
                                    string accountType = Console.ReadLine();
                                    switch (accountType)
                                    {
                                        case "1":
                                            Accounts.OpenNewAccount(loggedInUser);
                                            break;

                                        case "2":
                                            Accounts.OpenSavingsAccount(loggedInUser);
                                            break;

                                        default:
                                            Console.Clear();
                                            Console.WriteLine("\nChoose between 1-2!");
                                            Thread.Sleep(2000);
                                            break;
                                    }
                                    break;
                                case "2": //Pressing "2" returns the user to main menu.
                                    valid = true;
                                    break;

                                default: // error handling
                                    Console.WriteLine("\nChoose between 1-2!");
                                    Thread.Sleep(2000);
                                    break;
                            }
                        }
                        break;

                    case "2": //The user can borrow money using the "BorrowMoney" method
                        Console.Clear();
                        Currency.BorrowMoney(loggedInUser, loggedInUser.Accounts);                       
                        break;

                    case "3": // The user can send money between accounts both to their own and to other users.
                        Console.Clear();
                        TransferData.Transfer(loggedInUser, users);
                        break;

                    case "4": //The user can log out and another user/admin can log in.
                        Console.Clear();
                        Console.WriteLine("\nYou are now logged out...");
                        Thread.Sleep(3000);
                        return;

                    default: // Error handling
                        Console.Clear();
                        Console.WriteLine("\nChoose between 1-4!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        public static void Run() // Main running method that runs the whole program.
        {
            List<User> users = Admin.InitializeUser();
            List<Admin> admins = Admin.InitializeAdmin();
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Blue; //These lines controll the color theme of the program.
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                WelcomeMenu();
                Console.WriteLine("\nAre you a Customer or Admin?" +
                    "\n\n[1] Customer" +
                    "\n[2] Admin"); // IN the starting menu the user can choose to log in as admin or a regular customer.
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1": 
                        User loggedInUser = User.LogIn(users);
                        if (loggedInUser != null)
                        {
                            MainMenu(loggedInUser, users);
                        }
                        break;
                    case "2":
                        Admin loggedInAdmin = Admin.LogIn(admins);
                        if (loggedInAdmin != null)
                        {
                            Admin.AdminMenu(loggedInAdmin, users);
                        }
                        break;
                }
            }
        }
    }
}
