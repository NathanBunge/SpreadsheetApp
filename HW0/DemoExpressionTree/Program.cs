using SpreadsheetEngine;
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

        ExpressionTree eTree = new ExpressionTree("0");

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
                        createNewTree();
                        break;
                    case 2:
                        setVariableName();
                        break;
                    case 3:
                        evaluateTree();
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

        void createNewTree()
        {
            Console.WriteLine("Enter new expression: ");
            eTree = new ExpressionTree(Console.ReadLine());
        }

        void setVariableName()
        {
            Console.WriteLine("Enter Variable Name: ");
            string vName = Console.ReadLine();

            Console.WriteLine("Enter Variable Value:");
            double vValue = double.Parse(Console.ReadLine());

            eTree.SetVariable(vName, vValue);
        }

        void evaluateTree()
        {
           Console.WriteLine(eTree.Evaluate());
        }
    }
}


