// See https://aka.ms/new-console-template for more information

//this is an output statement to display to
//  your console screen.
//syntax:  static-classname.methodname(argument)

Console.WriteLine("Hello, World!");

//variables
//C# is a strongly-typed language
//once a variable has been declare of a specific
//   datatype, that variable WILL be ONLY that datatype
//Special: var variable
//          this obtains its datatype went it if first
//          encountered in your program according to
//          the data type of the value

//syntax datatype variablename [= value];
//       datatype variablename [, variable2name, ...];

//defualt for numerics 0

double rawGrade;
int weight, maxGrade = 0;
double weightMark = 0.0;

//decimal literals need to be identified by having an m at the end
//  of the value
//decimal weightMark = 0.0m;

// default value for a string is null
// literal strings are encased in double quotes (")
// literal characters (char) are encased in single quotes (')
string courseID;

//class variables
//DateTime which holds both a date and time
//.Today has a date of today and a time of 00:00:00am
//.Now has a date of today and a time of this millesecond
DateTime theDate = DateTime.Today;

//Input values
//Console method ReadLine();
//all input comes in as a string.
//To convert to a specific datatype, investigate if the datatype
//  has a .Parse() method

string inputValue;

//Write() does NOT move to the next line
Console.Write("Enter the course id:\t");
inputValue = Console.ReadLine();
courseID = inputValue;
Console.Write("Enter course mark weight:\t");
inputValue = Console.ReadLine();
weight = int.Parse(inputValue);

//example of looping to obtain a valid input value
bool flag = false;
while (flag != true)
{
    Console.Write("Enter max mark :\t");
    inputValue = Console.ReadLine();
    //TryParse attempts to convert your value
    //  if successful, it places the converted value in the out variable
    //                 and returns a true value
    //  if unsuccessful, no conversion is done, the out variable receives no value
    //                  and returns a false value
    if (int.TryParse(inputValue, out maxGrade) )
    {
        if (maxGrade <= 0)
        {
            Console.WriteLine($"\n\tMax grade cannot be a negative number\n");
        }
        else
            flag=true;
    }
    else
    {
        Console.WriteLine($"\n\tMax grade is not a numeric\n");
    }
}

Console.Write("Enter raw score :\t");
inputValue = Console.ReadLine();
rawGrade = double.Parse(inputValue);

//when using a structure that could require a group of statement
//  to be execute you will need to place the group in a coding block {....}
//if you structure will only require one statement
//  to be executed then the coding block { } are optional

// if ()
//    statement 1; ONLY statement 1 is consider to be part of the if structure
//    statement 2:

// if ()
//  {
//    statement 1; 
//    statement 2:
//  }

//condition statement
//a) if (condition) [{] true path [}]
//b) if (condition) [{] true path [}] else [{] false path[}]
//c) if (condition) [{] true path [}] else if (condition) [{] true path [}] .... else [{] false path[}]
//d) result value = (condition) ? true value : false value;
// as long a the code for the true value or false value resolves to a 
//      single value, your statement is value
//  in the following example the "nested" t-condition will resolve
//      to either the true value or the false value which will be
//      the false value for the outer t-condition
// result value = (condition) ? true value : (condition) ? true value :  false value;

//logic operators
// and &&
// or ||
// not !
// bit-and &
// bit-or |

if (rawGrade < 0 || rawGrade > maxGrade)
{
    Console.WriteLine($"Your raw grade of {rawGrade} is invalid. " +
        $"You need a value between 0 and {maxGrade}.");
    //using a return or exit or break or continue command
    //within a coding structure will be considered unstructured code
    //AND
    //result in marks lost in evaluation
    //return;
}
else
{

    //Calculations
    //the result of your calculation is dependent on your
    //  variable data type
    //problem here is the calculation variables are integers
    //          therefore the calculation uses the rules of integer arth.
    //Solutions:
    //a) change the datatype of your variables
    //      may cause problems elsewhere in your code, REMEMBER to retest your program

    //b) if one of the variables' datatype is different then others
    //      and allows for increased numeric representation (ie integer to double)
    //      then C# will attempt to do the calculation at the greater
    //      numeric representation

    //c) use a type-cast on your field(s)
    //      a type-cast is a temporary internal changing of how to handle
    //      the variable's data
    weightMark = rawGrade / (double)maxGrade * weight;

    //output
    //string concatenation
    //formating numerics
    Console.WriteLine("\nYour mark in {0} is {1:##0.0}",courseID,weightMark);

    Console.WriteLine("\nYour mark in " +
                       courseID + " is " +
                       Math.Round(weightMark,1));

    Console.WriteLine($"\nYour mark in {courseID} is {weightMark.ToString("#0.0")}");
}



