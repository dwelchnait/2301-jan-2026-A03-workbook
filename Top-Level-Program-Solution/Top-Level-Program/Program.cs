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

int rawGrade;
int weight, maxGrade;
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
Console.Write("Enter the course id:/t");
inputValue = Console.ReadLine();
courseID = inputValue;
Console.Write("Enter course mark weight:\t");
inputValue = Console.ReadLine();
weight = int.Parse(inputValue);
Console.Write("Enter max mark :\t");
inputValue = Console.ReadLine();
maxGrade = int.Parse(inputValue);
Console.Write("Enter raw score :\t");
inputValue = Console.ReadLine();
rawGrade = int.Parse(inputValue);

//Calculations
weightMark = rawGrade / maxGrade * weight;

//output
//string concatenation
Console.WriteLine($"\nYour mark in {courseID} is {weightMark}");




