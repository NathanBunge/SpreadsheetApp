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