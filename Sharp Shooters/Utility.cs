
namespace Sharp_Shooters
{
    internal class Utility
    {
        public static void UniversalReadKeyMethod()//Two methods to clean up the code with ReadKey
        {
            Console.WriteLine("Invalid input. Please enter a valid option...");
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
        }

        public static void UniqueReadKeyMethod()
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
        }

        public static int HidePincode()//This method replaces the entered password with asterixes for more security
        {
            int pin = 0;
            string input = "";

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter && char.IsDigit(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter); //When the user presses enter the method continues and returns the pincode.

            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out pin);
            }

            Console.WriteLine(); // Move to the next line after password input

            return pin;
        }

    }
}
