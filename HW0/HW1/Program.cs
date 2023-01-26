// Nathan Bunge
// 1-26-2023
// Cpts 321 HW1
using HW1;

Console.WriteLine("Hello, World!");
Console.WriteLine("Enter list of numbers seperated by spaces");


//get user input
string i = Console.ReadLine();

//parse string into integers
string[] vs = i.Split(' ');

int[] convertedItems = Array.ConvertAll<string, int>(vs, int.Parse);

HW1.BST tree = new HW1.BST();

foreach (int item in convertedItems)
{
    Console.WriteLine(item);
    tree.insert(item);
}




tree.printTreeOrdered();

Console.WriteLine("Nodes: {0}", tree.numItems());
Console.WriteLine("Levels: {0}", tree.numLevels().ToString());
Console.WriteLine("Min levels: {0}", BST.minLevels(tree.numItems()));
