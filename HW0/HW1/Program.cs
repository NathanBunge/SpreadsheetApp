// Nathan Bunge
// 1-26-2023
// Cpts 321 HW1
using HW1;

Console.WriteLine("Hello, World!");
Console.WriteLine("Enter list of numbers seperated by spaces");


//get user input (can assume correct format
string input = Console.ReadLine();

//parse string into words
string[] words = input.Split(' ');

//convert words to integers
int[] convertedItems = Array.ConvertAll<string, int>(words, int.Parse);

//create tree
BST tree = new BST();

//add each number to tree
foreach (int item in convertedItems)
{
    Console.WriteLine(item);
    tree.insert(item);
}




tree.printTreeOrdered();

Console.WriteLine("Nodes: {0}", tree.numItems());
Console.WriteLine("Levels: {0}", tree.numLevels().ToString());
Console.WriteLine("Min levels: {0}", BST.minLevels(tree.numItems()));
