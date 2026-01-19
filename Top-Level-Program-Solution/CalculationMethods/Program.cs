// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine("Its a bunch of small worlds (after all)!!!");

//discover the components of a method
//
//within the program you have 2 major areas:
// Driver
//    control the processing of the program
//    declares the data storage required in the program
//    it calls any methods that are required to
//          complete the program process



// Methods
// individual coding components
// perform a single task
// called by the driver or by another method



//driver
//Calculator
//should be able to perform Addition, Subtraction, Multiplication, Division
//should take two numbers from the user
//should allow the user to choose which operation to perform
//should display the input numbers, operation and result

//declare the program driver variables
int num1 = 0;
int num2 = 0;
double result = 0.0;
string choice = ""; //empty string

//call a method
num1 = GetIntegerNumber("Enter your first calculation number:");
num2 = GetIntegerNumber("Enter your second calculation number:");

//get the desired calculator operation
//there is no parameters to this method (thus no arguments on call statement)
//it does return a value
choice = GetCalculatorOperation();

//Case statement for C#
// a single value compared to a number of possibilities
//      and a single one of the possibilities is matched to the argument value
switch (choice.ToLower())
{
    case "a":
        {
            result = Addition(num1, num2);
            Console.WriteLine($"\nThe sum of {num1} + {num2} = {(int)result}");
            break; //this is not unstructured code, it is  part of the switch structure
        }
    case "s":
        {
            break; 
        }
    case "m":
        {
            break;
        }
    case "d":
        {
            break;
        }
    default:
        {
            //this last "case" is the "fall-thru" if no previous case was executed
            //this "case" is typically used for invalid messages
            Console.WriteLine($"\nYour operator choice of >{choice}< is invalid");
            break;
        }
}



//end of driver , program terminates

//Method Area

//create a component of code to do a specific task
//Task: GetIntegerNumber
//  receive a prompt line
//  display the prompt line
//  read the user input
//  (some validation may be included)
//  return a integer value

//Method Header
// a) the return value
// b) methodname
// c) list of input parameters
//syntax::    rdt methodname ([list of parameters]) { coding block.. }

//rdt : single value
//      valid C# datatype
//      if method returns no value use the key word-> void

//methodname: program developer defined
//            should be meaningful
//            Pascal Case

//list of parameters: optional
//                    syntax: datatype parametername
//                    separate parameters with a comma ,

//RULE:
// the order of incoming parameters DICTATE the order of the arguments
//      on the call statement
// the argument/parameter pair MUST have the same datatype

//Is datatype IMPORTANT
//VALUE type parameters receive a COPY of the argument value
//          WARNING any changes to the COPY will NOT be reflected back
//                  in the original value
//      sample: numerics, strings, bool, datetime, etc
//REFERENCE type parameters pass in the address of the original data
//          WARNING any changes to the data at the original address
//                  will be there when you return to the driver program
//      sample: object instance

//the use of the keyword "static" isolates the content of the method
//      to within the method. It DOES NOT allow for global variables
//      It forces the developer to properly scope the variables for 
//      the method.
//using static is an optional choice. 
//
//FOR OUR COURSE TO ENSURE APPROPRIATE METHOD DEVELOPMENT, WE WILL USE
//  THE KEYWORD "static" ON OUR METHODS

static int GetIntegerNumber(string prompt)
{
    //any variable declared within this method
    //   "dies" when the method terminates (scope)
    //treat your parameters as if they are local variables
    //   which means they are already declared
    string inputValue = "";
    int localNumber = 0;
    Console.Write($"\n{prompt}\t");
    inputValue = Console.ReadLine();
    localNumber = int.Parse( inputValue ); //assuming valid data entered


    //the method has stated a return datatype of int
    //this method must return an integer value
    //keyword -> return value;
    return localNumber;
}

static string GetCalculatorOperation()
{
    //any variable declared within this method has no association
    //  with any variable outside of the method
    //variables within a method may have the same name as a variable
    //  outside of the method
    //these variables are independent (restriction due to "static")

    //NOTE: NO VALIDATION IS BEING DONE IN THESE EXAMPLES
    string choice = "";
    Console.WriteLine("\nCalculator Operations");
    Console.WriteLine("a: Addition");
    Console.WriteLine("s: Subtration");
    Console.WriteLine("m: Multiplation");
    Console.WriteLine("d: Division");
    Console.Write("Enter your operator choice:\t");

    choice = Console.ReadLine();

    return choice;
}

static int Addition(int num1, int num2)
{
    //the parameters num1 and num2 are considered declared for 
    //      the method
    //these parameter/variables "live" as long as the method is
    //      executing
    //once the method is finished executing the local parameter/variables
    //      cease to exist, they "die"
    //the parameters in this method are "value-type" parameters
    //therefore they receive a "copy" of the value from the call statement
    //Any alteration of the local copies DO NOT affect the original values
    //num1 and num2 are NOT num1 and num2 of the driver routine
    return num1 + num2;
}