// See https://aka.ms/new-console-template for more information
using LearningObjects;

// Console is a static class
// it's instance does not need to be created to be able to use
// for a static class, use the class name and the property/method desired
Console.WriteLine("Hello, World!");

//reference a class within the LearningObjects namespace
//when declaring an instance of a class, referred to as the object,
//  use the class name as the datatype.
//by default a variable that is able to hold an instance of a class
//  AND is not instaniated  will have a value of null
Person Me; //currently Me is null

//get an instance of Person
//use the new command to create your instance
//when the new command executes, it looks at the class and
//  executes within the class a constructor
//NOTE: the user CANNOT call a constructor directly

Me = new Person(); //default constructor

Console.WriteLine($"\nThe contents of my default object (instance) is {Me.ToString()}");

Me = new Person("Don", 70, 101050.00m); //greedy constructor
Console.WriteLine($"\nThe contents of my greedy object (instance) is {Me.ToString()}");

Me = new Person("Don", 70); //greedy constructor using default wage
Console.WriteLine($"\nThe contents of my greedy object (instance) is {Me.ToString()}");

//this statement retreives the name on the instance
//this statement will use the get of the Name property
Console.WriteLine($"\nThe contents of Name on my instance (object) is {Me.Name}");

//when the property is on the receiving side of a statement
//  the property is using the set (set of Name property)
Me.Name = "   Terry    ";
Console.WriteLine($"The new contents of Name on my instance (object) is {Me.Name}");

//what happens if I attempt to assign an invalid value to a field on your instance
//example of user friendly error handling
//problem: a negative age will cause an exception to be thrown by the class
//         the class cannot (should not) attempt to display the error message to
//              to the console itself
//solution is to use try/catch code to handle the thrown exception

try
{
    //place the attempted code within the try coding block
    //Me.Age = -50;
    Me.Name = null;
}
catch (ArgumentNullException ex)
{
    //OPTIONAL
    //ArgumenNulltException is a more specific exception indentifier
    //  then ArgumentException
    Console.WriteLine($"\n\tMissing Data: {ex.Message}");
}
catch (ArgumentException ex)
{
    //OPTIONAL
    //ArgumentException is a more specific exception indentifier
    //it MUST be code prior to the general exception catch
    //this is the coding block that handles the thrown exception
    Console.WriteLine($"\n\tData Error: {ex.Message}");
}
catch (Exception ex)
{
    //the Exception instance is the "catch all" general exception in C#
    //this is the coding block that handles the thrown exception
    Console.WriteLine($"\n\tSystem Error: {ex.Message}");
}
