
namespace Sharp_Shooters
{
    internal class Utility
    {
        public static void UniversalReadKeyMeth()//Two methods to clean up the code with ReadKey
        {
            Console.WriteLine("Invalid input. Please enter a valid option...");
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
        }

        public static void UniqueReadKeyMeth()
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
        }

    }
}
