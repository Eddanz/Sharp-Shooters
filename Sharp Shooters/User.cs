

namespace Sharp_Shooters
{
    internal class User
    {
        

        public string UserName {  get; set; }
        public int PinCode { get; set; }
        public List<Accounts> Accounts { get; set; }

        
        public User(string name, int password, List<Accounts> accounts)
        {
            UserName = name;
            PinCode = password;            
            Accounts = accounts;
        }

        public void AccountOverview()
        {
            Console.WriteLine($"The bankaccounts for: {UserName}:");
            
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Account: {account.AccountName}\nBalance: {account.AccountBalance}");
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
                        Console.WriteLine($"\nWrong username or pincode.\nYou have {loginAttempts} attempts left!\nPress enter to continue");
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
        public void InternalTransfer()
        {
            foreach (var account in Accounts)
            {
                Console.WriteLine(account.AccountName);
            }
        }

        public void ExternalTransfer()
        {

        }

        public void LoanMoney()
        {

        }
    }

    



    
}
