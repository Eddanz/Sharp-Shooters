

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
            Console.WriteLine($"Dina bankkonton för {UserName}:");
            
            foreach (var account in Accounts)
            {
                Console.WriteLine($"Konto: {account.AccountName}\nSaldo: {account.AccountBalance}");
            }
        }

        public static User LogIn(List<User> users)
        {

            string enterName ="";
            int loginAttempts = 3; //Hur många försök användaren har att försöka logga in
            while (loginAttempts != 0) //While-loop som körs så länge försöken som är kvar inte är noll
            {
                Console.Clear();
                Console.Write("Användarnamn: ");
                enterName = Console.ReadLine().ToLower();
                Console.Write("Pinkod: ");
                if (int.TryParse(Console.ReadLine(), out int enterPincode))
                {
                    //Med hjälp av FirstOrDefualt och lambda-uttryck så söker den igenom listan av användare och ser om den hittar matchning med inmatat användar namn och pinkod,
                    //returnerar sedan en inloggad användare som tilldelas till loggedInUser.
                    User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.PinCode == enterPincode);

                    if (loggedInUser != null) //Om loggedInUser tilldelats en användare
                    {
                        Console.Clear();
                        Console.WriteLine($"\nInloggningen lyckades, varmt välkommen {loggedInUser.UserName.ToUpper()}!" +
                            $"\nVänligen vänta medan jag hämtar dina konton...");
                        Thread.Sleep(3000);
                        return loggedInUser; //Returnerar den inloggade användaren
                    }
                    else //Om användaren skriver in fel användarnamn eller pinkod
                    {
                        loginAttempts--; //Tar bort ett inloggningsförsök
                        Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Fel användarnamn eller pinkod.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                        Console.ReadLine();
                    }
                }
                else //Om användaren skriver in bokstäver i pinkoden
                {
                    loginAttempts--; //Tar bort ett inloggningsförsök
                    Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Pinkod kan bara vara siffror.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                    Console.ReadLine();
                }
            }
            if (loginAttempts == 0) //Om inloggningsförsöken är slut
            {
                User lockedUser = users.FirstOrDefault(u => u.UserName == enterName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Försök är slut! Programmet stängs ner...");
                users.Remove(lockedUser);
                Console.WriteLine("Användaren är nu låst. Kontakta systemadministratören för att låsa upp kontot.");
            }
            return null;
        }
        public void OpenNewAccount(User user)//Method to open a new account
        {
            Console.Clear();
            Console.Write("Name the account: ");
            string accountName = Console.ReadLine();

            Console.Write($"Deposit Cashish in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit);
                user.Accounts.Add(newAccount);

                Console.WriteLine($"{accountName} has been created with a balance of {deposit:C}.");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }
        public void OpenSavingsAccount(User user)//Method to open a savings account
        {
            Console.Clear();
            Console.Write("Name the account: ");
            string accountName = "Savings Account";
            double interest = 0.035;

            Console.Write($"Deposit Cashish in {accountName}: ");
            if (double.TryParse(Console.ReadLine(), out double deposit))
            {
                Accounts newAccount = new Accounts(accountName, deposit);
                user.Accounts.Add(newAccount);

                Console.WriteLine($"{accountName} has been created with a balance of {deposit:C}.");
                Console.WriteLine($"The interest on your {accountName} will be: {deposit * interest:C}");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();
            }
        }
    }

        //public void InternalTransfer()
        //{

        //}

        //public void ExternalTransfer()
        //{

        //}
    
        //public void LoanMoney()
        //{
            
        //}

        

    //}
}
