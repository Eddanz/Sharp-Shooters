

namespace Sharp_Shooters
{
    internal class Bank
    {
        static void WelcomeMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to SharpShooter Bank");
            Console.WriteLine("----------------------------");
            Console.WriteLine("                 _ _.-'`-._ _\r\n                ;.'________'.;\r\n     _________n.[____________].n_________\r\n    |\"\"_\"\"_\"\"_\"\"||==||==||==||\"\"_\"\"_\"\"_\"\"]\r\n    |\"\"\"\"\"\"\"\"\"\"\"||..||..||..||\"\"\"\"\"\"\"\"\"\"\"|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n    |.. .. .. ..||..||..||..||.. .. .. ..|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n ,,;;,;;;,;;;,;;;,;;;,;;;,;;;,;;,;;;,;;;,;;,,\r\n;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
            Console.WriteLine("\nPress Enter to Log in!");
            Console.ReadKey();
        }

        static void MainMenu(User loggedInUser, List<User> users)
        {
            while (true)
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
                    case "1":
                        Console.Clear();
                        User.AccountOverview(loggedInUser);
                        Console.WriteLine("\n[1] Open a new account" +
                            "\n[2] Go back to main menu");
                        string userChoise1 = Console.ReadLine();
                        switch (userChoise1)
                        {
                            case "1":
                                Console.Clear();
                                Console.WriteLine("[1] Open a regular account" +
                                    "\n[2] Open a savings account");
                                string accountType = Console.ReadLine();
                                switch (accountType)
                                {
                                    case "1":
                                        User.OpenNewAccount(loggedInUser);
                                        break;
                                    case "2":
                                        User.OpenSavingsAccount(loggedInUser);
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("\nChoose between 1-2!");
                                        Thread.Sleep(2000);
                                        break;
                                }
                                break;
                            case "2":
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("\nChoose between 1-2!");
                                Thread.Sleep(2000);
                                break;
                        }
                        break;
                    case "2":
                        User.BorrowMoney(loggedInUser);
                        //User.LoanMoney();
                        break;
                    case "3":
                        Console.Clear();
                        User.Transfer(loggedInUser, users);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("You are now logged out...");
                        Thread.Sleep(3000);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nChoose between 1-4!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        public static void Run()
        {
            List<User> users = Admin.InitializeUser();
            while (true)
            {
                WelcomeMenu();
                User loggedInUser = User.LogIn(users);
                if (loggedInUser != null)
                {
                    MainMenu(loggedInUser, users);
                }
            }
        }
    }
}
