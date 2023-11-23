

namespace Sharp_Shooters
{
    internal class Admin
    {
        public static void InitializeUser()
        {
            List<Accounts> TheoAccounts = new List<Accounts> //Lista med konton som tillhör användaren "Theo"
            {
                new Accounts("Account 1", 111111) //Skapar nytt objekt från Accounts-klassen
            };
            List<Accounts> EddieAccounts = new List<Accounts> //Lista med konton som tillhör användaren "Eddie"
            {
                new Accounts("Account 1", 111111) //Skapar nytt objekt från Accounts-klassen
            };
            List<Accounts> TorBjornAccounts = new List<Accounts> //Lista med konton som tillhör användaren "Torbjörn"
            {
                new Accounts("Account 1", 111111) //Skapar nytt objekt från Accounts-klassen
            };
            List<Accounts> SimonAccounts = new List<Accounts> //Lista med konton som tillhör användaren "Simon"
            {
                new Accounts("Account 1", 111111) //Skapar nytt objekt från Accounts-klassen
            };

            List<User> users = new List<User> //Lista med användare
            {
                new User("theo", 1111, TheoAccounts), //Skapar nya objekt från User-klassen
                new User("eddie", 2222, EddieAccounts),
                new User("tor bjorn", 3333 , TorBjornAccounts),
                new User("simon", 4444, SimonAccounts)

            };
            
        }
        
        public void TranserTest()
        {
            
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
