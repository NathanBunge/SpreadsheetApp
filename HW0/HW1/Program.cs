// Nathan Bunge
// 1-26-2023
// Cpts 321 HW1


Console.WriteLine("Hello, World!");
Console.WriteLine("Enter list of numbers seperated by spaces");


//get user input
string i = Console.ReadLine();

//parse string into integers
string[] vs = i.Split(' ');

int[] convertedItems = Array.ConvertAll<string, int>(vs, int.Parse);

foreach (int item in convertedItems)
{
    Console.WriteLine(item);
}


HW1.BST tree = new HW1.BST();
tree.insert(1);
tree.insert(2);
tree.insert(3);
tree.insert(4);


tree.printTreeOrdered();

Console.WriteLine(tree.numItems().ToString());
Console.WriteLine(tree.numLevels().ToString());
