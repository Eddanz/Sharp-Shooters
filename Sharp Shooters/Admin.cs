

using System.Reflection.Metadata;

namespace Sharp_Shooters
{
    internal class Admin
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }

        public Admin(string username, int pincode)
        {
            UserName = username;
            PinCode = pincode;
        }
        public static List<User> InitializeUser()
        {
            List<Accounts> TheoAccounts = new List<Accounts> //List with accounts belonging to user: "Theo"
            {
                new Accounts("Salary £: ", 10000, "euro"), //Creates a new account from the Accounts Class
                new Accounts("Savings $: ", 1337, "dollar"), //Dollar
                new Accounts("Muay Thai SEK: ", 5, "kronor")
            };

            List<Accounts> EddieAccounts = new List<Accounts> //List with accounts belonging to user:"Eddie"
            {
                new Accounts("Salary £: ", 111111, "euro"),
                new Accounts("Savings $: ", 20, "dollar"),
                new Accounts("CS Skins SEK: ", 15000, "kronor")
            };

            List<Accounts> TorBjornAccounts = new List<Accounts> //List with accounts belonging to user: "Torbjörn"
            {
                new Accounts("Salary £: ", 111111, "euro"), 
                new Accounts("Savings $: ", 500, "dollar"),
                new Accounts("Snus SEK: ", 2050, "kronor")
            };

            List<Accounts> SimonAccounts = new List<Accounts> //List with accounts belonging to user: "Simon"
            {
                new Accounts("Salary £: ", 111111, "euro"), 
                new Accounts("Savings $: ", 67000, "dollar"), 
                new Accounts("CS Inventory SEK: ", 1000, "kronor"),
                new Accounts("Floorball SEK: ", 50, "kronor"), 
            };

            List<User> users = new List<User> //List of users
            {
                new User("theo", 1111, TheoAccounts), //Created new objects from the user-class
                new User("eddie", 2222, EddieAccounts),
                new User("tor bjorn", 3333 , TorBjornAccounts),
                new User("simon", 4444, SimonAccounts)

            };

            return users;
        }

        public static List<Admin> InitializeAdmin()
        {
            List<Admin> admins = new List<Admin>
            {
                new Admin("admin", 0000)
            };

            return admins;
        }

        public static Admin LogIn(List<Admin> admins)
        {

            string enterName = "";
            int loginAttempts = 3; //The attempts the user has to login.
            while (loginAttempts != 0) //While-loop that runs as long as the login attempts are not 0.
            {
                Console.Clear();
                Console.Write("Username: ");
                enterName = Console.ReadLine().ToLower();
                Console.Write("Pincode: ");
                if (int.TryParse(Console.ReadLine(), out int enterPincode))
                {
                    //Using the FirstOfDeafult and Lambda expression it searches through the list of admins and looks for a matching username and pincode, Return the the admin as loggedInAdmin.

                    Admin loggedInAdmin = admins.FirstOrDefault(a => a.UserName == enterName && a.PinCode == enterPincode);

                    if (loggedInAdmin != null) //If loggedInAdmin has returned a value
                    {
                        Console.Clear();
                        Console.WriteLine($"\nLog in succesfull, Welcome {loggedInAdmin.UserName.ToUpper()}!" +
                            $"\nPlease wait while the information is retrived...");
                        Thread.Sleep(2000);
                        return loggedInAdmin; //Returns the loggedInAdmin

                    }
                    else //If the user enters the wrong credentials
                    {
                        loginAttempts--; //Remove one login attempt
                        Console.WriteLine($"\nWrong Credentials or you may have been blocked. \n If this problems continues, contact an administrator. \nYou have {loginAttempts} attempts left!\nPress enter to continue");
                        Console.ReadLine();
                    }
                }
                else //IF the admin writes something else then numbers
                {
                    loginAttempts--; //Remove one login attempt
                    Console.WriteLine($"\nUnsuccesfull login. The Pincode can only contain numbers.\nYou have {loginAttempts} attempts left!\nPress enter to continue");
                    Console.ReadLine();
                }
            }
            if (loginAttempts == 0) //IF the login attempts reaches 0
            {
                Admin lockedAdmin = admins.FirstOrDefault(u => u.UserName == enterName);//Search for the username in the list.
                Console.ForegroundColor = ConsoleColor.Red;//Change the text to red 
                Console.WriteLine("No more tries...");
                admins.Remove(lockedAdmin);//Remove the admin in then list so they cant login again.
                Console.WriteLine("The user is now locked. Contact an Admin to solve the issue..");
            }
            return null;
        }

        public static void CreateUser(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("===== Create a new user =====\n" +
                "What is the name of the user?");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("What pincode should the user have?");
            int.TryParse(Console.ReadLine(), out int pincode);
            List<Accounts> accountName = new List<Accounts>();
            User user = new User(name, pincode, accountName);
            users.Add(user);
        }

        public static void UpdateCurrency()
        {
            Console.Clear();
            double DollarCur = 0.1;
            double EuroCur = 0.091;
            double SekCur = 1;
            Console.WriteLine("Update currency \n [1] (£) Euro \n [2] ($) Dollar \n [3] (SEK) Kronor");
            var CAnswer = Console.ReadLine();
            switch (CAnswer)
            {
                case "1":
                    Console.WriteLine("What is the current exchange rate for (£) Euro?");

                    double.TryParse(Console.ReadLine(),out EuroCur);
                    break;
                case "2":
                    Console.WriteLine("What is the current exchange rate for (£) Euro?");

                    double.TryParse(Console.ReadLine(), out DollarCur);
                    break;
                case "3":
                    Console.WriteLine("What is the current exchange rate for (£) Euro?");

                    double.TryParse(Console.ReadLine(), out SekCur);
                    break;
                default:
                    Console.WriteLine("You must choose 1-3!");
                    break;
            }
        }

        public static void AdminMenu(Admin loggedInAdmin, List<User> users)
        {
            Console.Clear();
            Console.Write($"\n===== You are logged in as: {loggedInAdmin.UserName.ToUpper()} =====" +
                "\n[1] Create a new user" +
                "\n[2] Update currency" +
                "\n[3] Log out" +
                "\nCHOISE:");

            string adminChoise = Console.ReadLine();
            switch (adminChoise)
            {
                case "1":
                    CreateUser(users);
                    break;
                case "2":
                    UpdateCurrency();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("You are now logged out...");
                    Thread.Sleep(3000);
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nChoose between 1-3!");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}
