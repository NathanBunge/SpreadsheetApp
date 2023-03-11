
internal static class ExpressionDemo
{
    static void DisplayMenu()
    {
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Enter a new expression");
        Console.WriteLine("2. Set a variable value");
        Console.WriteLine("3. Evaluate Tree");
        Console.WriteLine("4. Quit");
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            Console.Write("Selection: ");

            int selection;
            if (int.TryParse(Console.ReadLine(), out selection))
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("You selected Option 1.");
                        break;
                    case 2:
                        Console.WriteLine("You selected Option 2.");
                        break;
                    case 3:
                        Console.WriteLine("You selected Option 3.");
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }

            Console.WriteLine();
        }
    }
}


