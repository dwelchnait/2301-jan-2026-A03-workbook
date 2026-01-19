// See https://aka.ms/new-console-template for more information
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

Console.WriteLine($"\n\tYou entered {num1}");

//end of driver

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