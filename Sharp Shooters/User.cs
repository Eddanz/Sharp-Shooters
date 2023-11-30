
namespace Sharp_Shooters
{
    internal class User //Defines what a user is with the properties below.
    {
        public string UserName { get; set; }
        public int PinCode { get; set; }
        public List<Accounts> Accounts { get; set; }
        public List<string> Transactions { get; set; }

        public User(string name, int pincode, List<Accounts> accounts, List<string> transactions) //Constructor for the users.
        {
            UserName = name;
            PinCode = pincode;
            Accounts = accounts;
            Transactions = transactions;
        }

        public static User LogIn(List<User> users)
        {
            string enterName = "";
            int loginAttempts = 3; //The attempts the user has to login.
            while (loginAttempts != 0) //While-loop that runs as long as the login attempts are not 0.
            {
                Console.Clear();
                Console.Write("\nUsername: ");
                enterName = Console.ReadLine().ToLower();
                Console.Write("Pincode: ");
                int enteredPincode = Utility.HidePincode();

                if (enteredPincode != null)
                {
                    //Using the FirstOfDeafult and Lambda expression it searches through the list of users and looks for a matching username and pincode, Return the the user as loggedInUser.
                    User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.PinCode == enteredPincode);

                    if (loggedInUser != null) //If loggedInUser has returned a value
                    {
                        Console.Clear();
                        Console.WriteLine($"\nLog in successful, Welcome {loggedInUser.UserName.ToUpper()}!" +
                            $"\nPlease wait while the information is retrieved...");
                        Thread.Sleep(2000);
                        return loggedInUser; //Returns the loggedInUser

                    }
                    else //If the user enters the wrong credentials
                    {
                        loginAttempts--; //Remove one login attempt
                        Console.WriteLine($"\nWrong Credentials or you may have been blocked. \nIf this problems continues, contact an administrator. \nYou have {loginAttempts} attempts left!\nPress enter to continue");
                        Console.ReadLine();
                    }
                }
                else //IF the user writes something else then numbers
                {
                    loginAttempts--; //Remove one login attempt
                    Console.WriteLine($"\nUnsuccessful login. The pincode can only contain numbers.\nYou have {loginAttempts} attempts left!\nPress enter to continue");
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
    }
}

