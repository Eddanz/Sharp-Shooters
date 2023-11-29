
namespace Sharp_Shooters
{
    internal class Admin //We declare what an Admin is with the properties below.
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }

        public Admin(string username, int pincode) // Constructor for the Admin
        {
            UserName = username;
            PinCode = pincode;
        }
        public static List<User> InitializeUser() //This mehtod creates all of the users and their accounts
        {
            List<Accounts> TheoAccounts = new List<Accounts> //List with accounts belonging to user: "Theo"
            {
                new Accounts("Salary:", 10000, "EURO", "£"), //Creates a new account from the Accounts Class
                new Accounts("Savings:", 1337, "USD", "$"), 
                new Accounts("Muay Thai:", 5, "KRONOR", "SEK")
            };
           
            List<Accounts> EddieAccounts = new List<Accounts> //List with accounts belonging to user:"Eddie"
            {
                new Accounts("Salary:", 111111, "EURO", "£"),
                new Accounts("Savings:", 20, "USD", "$"),
                new Accounts("CS Skins:", 15000, "KRONOR", "SEK")
            };
            
            List<Accounts> TorBjornAccounts = new List<Accounts> //List with accounts belonging to user: "Torbjörn"
            {
                new Accounts("Salary:", 111111, "EURO", "£"), 
                new Accounts("Savings:", 500, "USD", "$"),
                new Accounts("Snus:", 2050, "KRONOR", "SEK")
            };
            
            List<Accounts> SimonAccounts = new List<Accounts> //List with accounts belonging to user: "Simon"
            {
                new Accounts("Salary:", 111111, "EURO", "£"), 
                new Accounts("Savings:", 67000, "USD", "$"), 
                new Accounts("CS Inventory:", 1000, "KRONOR", "SEK"),
                new Accounts("Floorball:", 50, "KRONOR", "SEK"), 
            };

            List<string> SimonTransactions = new List<string>();
            List<string> TorBjornTransactions = new List<string>();
            List<string> EddieTransactions = new List<string>();
            List<string> TheoTransactions = new List<string>();

            List<User> users = new List<User> //List of users
            {
                new User("theo", 1111, TheoAccounts, TheoTransactions), //Created new objects from the user-class
                new User("eddie", 2222, EddieAccounts, EddieTransactions),
                new User("tor bjorn", 3333 , TorBjornAccounts, TorBjornTransactions),
                new User("simon", 4444, SimonAccounts, SimonTransactions)

            };
            return users;
        }

        public static List<Admin> InitializeAdmin() // This method creates the admin with the username and pincode
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

        public static void CreateUser(List<User> users) //This methods lets the Admin create a new user
        {
            Console.Clear();
            Console.WriteLine("===== Create a new user =====\n" +
                "What is the name of the user?");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine("What pincode should the user have?");
            int.TryParse(Console.ReadLine(), out int pincode);
            List<Accounts> accountName = new List<Accounts>();
            List<string> transactionName = new List<string>();
            User user = new User(name, pincode, accountName, transactionName);
            users.Add(user);
        }

        
        public static void AdminMenu(Admin loggedInAdmin, List<User> users) // the admin does not have any accounts or transfer. The admin only has "Create new user" and "Update Currency"
        {
            while (true)
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
                        Currency.UpdateCurrency();
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
}
