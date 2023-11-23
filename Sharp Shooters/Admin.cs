

namespace Sharp_Shooters
{
    internal class Admin
    {
        public static List<User> InitializeUser()
        {
            List<Accounts> TheoAccounts = new List<Accounts> //List with accounts belonging to user: "Theo"
            {
                new Accounts("Salary", 9999999), //Creates a new account from the Accounts Class
                new Accounts("Savings", 1337), //Dollar
                new Accounts("Savings", 1337)
            };
            List<Accounts> EddieAccounts = new List<Accounts> //List with accounts belonging to user:"Eddie"
            {
                new Accounts("Salary", 111111), 
                new Accounts("Savings", 22),
                new Accounts("CS Skins", 22)
            };
            List<Accounts> TorBjornAccounts = new List<Accounts> //List with accounts belonging to user: "Torbjörn"
            {
                new Accounts("Salary", 111111), 
                new Accounts("Savings", 22),
                new Accounts("Snus", 2050),
            };
            List<Accounts> SimonAccounts = new List<Accounts> //List with accounts belonging to user: "Simon"
            {
                new Accounts("Salary", 111111), 
                new Accounts("Savings", 67000), 
                new Accounts("CS Inventory", 1000), //Dollar
                new Accounts("Floorball", 50), 
            };
            List<Accounts> AllAccounts = new List<Accounts>();
            AllAccounts.Add(TheoAccounts[0]);
            AllAccounts.Add(TheoAccounts[1]);
            AllAccounts.Add(EddieAccounts[0]);
            AllAccounts.Add(EddieAccounts[1]);
            AllAccounts.Add(TorBjornAccounts[0]);
            AllAccounts.Add(TorBjornAccounts[1]);
            AllAccounts.Add(SimonAccounts[0]);
            AllAccounts.Add(SimonAccounts[1]);

            List<User> users = new List<User> //List of users
            {
                new User("theo", 1111, TheoAccounts), //Created new objects from the user-class
                new User("eddie", 2222, EddieAccounts),
                new User("tor bjorn", 3333 , TorBjornAccounts),
                new User("simon", 4444, SimonAccounts)

            };
            
            return users;
        }
        
        

        public void CreateUser()
        {
            Console.WriteLine("===== Create a new user =====\n" +
                "What is the name of the user?");
            string name = Console.ReadLine();
            Console.WriteLine("what pincode should the user have?");
            int pincode = Convert.ToInt32(Console.ReadLine());
            

        }

        public void UpdateCurrency()
        {
            
        }
    }
}
