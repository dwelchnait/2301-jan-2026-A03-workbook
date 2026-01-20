// See https://aka.ms/new-console-template for more information
Console.WriteLine("Over, over and over again!!!");

//typical menu loop
//post-test loop
//do-until loops

string choice = "";

//post-test iteration
//will execute the coding block at least once
//after each execution of the coding block the
//  iteration condition is evaluated
//if true, do the iteration again
//if false, exit the iteration
do
{
    choice = GetChoice();
    switch (choice.ToUpper())
    {
        case "A":
            {
                WhileIteration();
                break;
            }
        case "B":
            {
                ForIteration();
                break;
            }
        case "C":
            {
                ForEachIteration();
                break;
            }
        case "X":
            {
                Console.WriteLine("\n Thank you come again.");
                break;
            }
        default:
            {
                Console.WriteLine($"\nYour iteration choice of >{choice}< is invalid");
                break;
            }
    }

} while (choice.ToUpper() != "X");

static string GetChoice()
{
    string choice = "";
    Console.WriteLine("\nChoose pre-text iteration example:");
    Console.WriteLine("a: While");
    Console.WriteLine("b: For exact loop iteration");
    Console.WriteLine("c: Foreach (collections)");
    Console.WriteLine("x: Exit");
    Console.Write("Enter your choice:\t");

    choice = Console.ReadLine();

    return choice;
}

//WARNING!!!

//the use of unstructure code to do a quick exit from the iteration
//  will be considered a major error FOR EACH occurance

//to do a quick exit from an iteration, manipulate the termination condition

static void WhileIteration()
{
    //collect a set of numbers and display the total
    string inputValue = "";
    int total = 0;
    int num = 0;
    int numberOfValues = 0;

    Console.Write($"\nEnter a positive integer value or -1 to exit:\t");
    inputValue = Console.ReadLine();
    num = int.Parse(inputValue);

    //pre-test iteration, the loop condition is done before the coding block is executed
    //the iteration continues as long as the condition is true
    //the while iteration is used when the number of iterations is unknown

    while (num >= 0) // num == -1
    {
        //total = total + num;
        total += num;
        numberOfValues++; // numberOfValues = numberOfValues + 1;

        Console.Write($"\nEnter a positive integer value or -1 to exit:\t");
        inputValue = Console.ReadLine();
        num = int.Parse(inputValue);
    }

    Console.WriteLine($"\n\tThe sum of the {numberOfValues} digits entered is {total}.");
}

static void ForIteration()
{
    //collect a set of numbers and display the total
    string inputValue = "";
    int total = 0;
    int num = 0;
    int numberOfValues = 0;

    Console.Write($"\nEnter the number of iterations to do:\t");
    inputValue = Console.ReadLine();
    numberOfValues = int.Parse(inputValue);

    //pre-test iteration, the loop condition is done before the coding block is executed
    //the iteration continues as long as the condition is true
    //the for iteration is used when the number of iterations is known
    //syntax  for(int loopcounter = value; termination condition; increment/decrement)
    //           { coding block }

    

    for (int counter = 0; counter < numberOfValues; counter++) // num == -1
    {
        Console.Write($"\nEnter a integer value:\t");
        inputValue = Console.ReadLine();
        num = int.Parse(inputValue);
        total += num;
    }

    Console.WriteLine($"\n\tThe sum of the {numberOfValues} digits entered is {total}.");
}

static void ForEachIteration()
{
    //collect a set of numbers and display the total
    string inputValue = "";
    int total = 0;
    int[] numberOfValues = new int[] { 1, 2, 3, 4, 5};
    int numberOfCollectionItems = 0;

    //pre-test iteration, the loop condition is done before the coding block is executed
    //the iteration starts with the first item in your collection, continues processing
    //  the next item on the next iteration, and continues until the last item is processed
    //if there is no items in the collection, no iteration is done
    //syntax  foreach(collectiondatatype collectionitemidentifier in collectionname)
    //           { coding block }
    //typically you may see the datatype var used as the collectiondatatype



    foreach (int num in numberOfValues) // num will hold the current processing collection item
    {
        //note no input, data is already within a stored collection
       total += num;
        numberOfCollectionItems++;
    }

    Console.WriteLine($"\n\tThe sum of the {numberOfCollectionItems} collection digits is {total}.");
}
