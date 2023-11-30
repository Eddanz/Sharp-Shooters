
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
        public static List<User> InitializeUser() //This method creates all of the users and their accounts
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

            List<string> SimonTransactions = new List<string>(); //List that saves all of the transactions made by the user.
            List<string> TorBjornTransactions = new List<string>();
            List<string> EddieTransactions = new List<string>();
            List<string> TheoTransactions = new List<string>();

            List<User> users = new List<User> //List of users
            {
                new User("theo", 1111, TheoAccounts, TheoTransactions), //Created new objects from the user-class
                new User("eddie", 2222, EddieAccounts, EddieTransactions),
                new User("torbjorn", 3333 , TorBjornAccounts, TorBjornTransactions),
                new User("simon", 4444, SimonAccounts, SimonTransactions)

            };
            return users;
        }

        public static List<Admin> InitializeAdmin() // This method creates the admin with the username and pincode
        {
            List<Admin> admins = new List<Admin>
            {
                new Admin("admin", 1337)
            };

            return admins;
        }

        public static Admin LogIn(List<Admin> admins) 
        {

            string enterName = "";
            while (true) //While-loop that runs as long as the login attempts are not 0.
            {
                Console.Clear();
                Console.Write("Username: ");
                enterName = Console.ReadLine().ToLower();
                Console.Write("Pincode: ");
                int enteredPincode = Utility.HidePincode();
                if (enteredPincode != null)
                {
                    //Using the FirstOrDeafult and Lambda expression it searches through the list of admins and looks for a matching username and pincode, Return the the admin as loggedInAdmin.

                    Admin loggedInAdmin = admins.FirstOrDefault(a => a.UserName == enterName && a.PinCode == enteredPincode);

                    if (loggedInAdmin != null) //If loggedInAdmin has returned a value
                    {
                        Console.Clear();
                        Console.WriteLine($"\nLog in successful, Welcome {loggedInAdmin.UserName.ToUpper()}!" +
                            $"\nPlease wait while the information is retrieved...");
                        Thread.Sleep(2000);
                        return loggedInAdmin; //Returns the loggedInAdmin

                    }
                    else //If the user enters the wrong credentials
                    {
                        Console.WriteLine($"\nWrong Credentials \nPress enter to continue");
                        Console.ReadLine();
                    }
                }
                else //IF the admin writes something else then numbers
                {
                    
                    Console.WriteLine($"\nUnsuccessful login. The pincode can only contain numbers.\nPress enter to continue");
                    Console.ReadLine();
                }
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
                    "\n\n[1] Create a new user" +
                    "\n[2] Update currency" +
                    "\n[3] Log out" +
                    "\n\nCHOISE:");

                string adminChoice = Console.ReadLine();
                switch (adminChoice)
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
