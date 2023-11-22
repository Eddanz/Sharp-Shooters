

namespace Sharp_Shooters
{
    internal class Bank
    {
        public static void MainMenu()
        {
            bool isLoggedIn = false;
            while (!isLoggedIn)
            {
                Console.WriteLine("Welcome to SharpShooter Bank");
                Console.WriteLine("----------------------------");
                Console.WriteLine("                 _ _.-'`-._ _\r\n                ;.'________'.;\r\n     _________n.[____________].n_________\r\n    |\"\"_\"\"_\"\"_\"\"||==||==||==||\"\"_\"\"_\"\"_\"\"]\r\n    |\"\"\"\"\"\"\"\"\"\"\"||..||..||..||\"\"\"\"\"\"\"\"\"\"\"|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n    |.. .. .. ..||..||..||..||.. .. .. ..|\r\n    |LI LI LI LI||LI||LI||LI||LI LI LI LI|\r\n ,,;;,;;;,;;;,;;;,;;;,;;;,;;;,;;,;;;,;;;,;;,,\r\n;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                Console.WriteLine("\nPress Enter to Log in!");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
