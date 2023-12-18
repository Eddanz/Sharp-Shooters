
using System.Reflection.Metadata;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace Sharp_Shooters
{
    internal class User //Defines what a user is with the properties below.
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }
        public List<Accounts> Accounts { get; set; }
        public List<string> Transactions { get; set; }
        public double InitialTotalBalance { get; set; }

        private static Dictionary<string, int> failedLoginAttempts = new Dictionary<string, int>();

        public static List<User> blockedUsers = new List<User>();

        public User(string name, int pincode, List<Accounts> accounts, List<string> transactions, double initialTotalBalance) //Constructor for the users.
        {
            UserName = name;
            PinCode = pincode;
            Accounts = accounts;
            Transactions = transactions;
            InitialTotalBalance = initialTotalBalance;
        }

        public static User LogIn(List<User> users)
        {

            while (true)
            {
                Console.Clear();
                Console.Write("\nUsername: ");
                string enterName = Console.ReadLine().ToLower();

                if (!failedLoginAttempts.ContainsKey(enterName))
                {
                    failedLoginAttempts[enterName] = 0; // Initialize failed attempts for the user
                }

                Console.Write("Pincode: ");
                int enteredPincode = Utility.HidePincode();

                if (enteredPincode != null)
                {
                    //Using the FirstOfDeafult and Lambda expression it searches through the list of users and looks for a matching username and pincode, Return the the user as loggedInUser.
                    User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.PinCode == enteredPincode);

                    if (loggedInUser != null)
                    {
                        Console.Clear();
                        Console.WriteLine($"\nLog in successful, Welcome {loggedInUser.UserName.ToUpper()}!" +
                        $"\nPlease wait while the information is retrieved...");
                        Thread.Sleep(2000);
                        failedLoginAttempts[enterName] = 0; // Reset failed login attempts upon successful login
                        return loggedInUser;

                    }
                    else
                    {
                        failedLoginAttempts[enterName]++; // Increment failed login attempts for the user

                        if (failedLoginAttempts[enterName] == 3)
                        {
                            User lockedUser = users.FirstOrDefault(a => a.UserName == enterName);//Search for the username in the list.
                            if (lockedUser != null)
                            {
                                users.Remove(lockedUser);//Remove the user in then list so they cant login again.
                                blockedUsers.Add(lockedUser);
                                Console.WriteLine($"\nNo more tries for {enterName.ToUpper()}...");
                                Console.WriteLine($"The user {enterName.ToUpper()} is now locked. Contact an Admin to solve the issue..");
                                Utility.UniqueReadKeyMethod();
                                return null; // Lock the user and exit the method
                            }
                        }
                        else if (failedLoginAttempts[enterName] > 3)
                        {
                            Console.WriteLine($"\nNo more tries for {enterName.ToUpper()}...");
                            Console.WriteLine($"The user {enterName.ToUpper()} is now locked. Contact an Admin to solve the issue..");
                            Utility.UniqueReadKeyMethod();
                            return null; // Lock the user and exit the method
                        }

                        Console.WriteLine($"\nWrong Credentials or you may have been blocked. \nIf this problem continues, contact an administrator. \nYou have {3 - failedLoginAttempts[enterName]} attempts left!\nPress enter to continue");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}

