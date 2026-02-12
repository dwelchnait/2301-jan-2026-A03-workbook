// See https://aka.ms/new-console-template for more information
//all using namespace statement must be at the top of your code
#region Additonal Namespaces
using SQLiteDemos;
#endregion

Console.WriteLine("Hello, World!");


//use the class and context class to write and retreive records (instances)
//  from a sqlite db (which is actually just a file)

//two commands that can be execute to manage your sqlite db file
//      via an instance of your context class (context) are:
//  context.DataBase.EnsureCreate
//      this command will check to see if the db file exists
//      if not, it is created based on the entities of your context class model
//      if it exists, it does nothing
//  context.Database.EnsureDelete
//      this command will delete your existing dbFile of your context class
//      this command is useful if you wish to remove the file

//your program needs to create an instance of your context class
//  to be able to interact with sqllite

using AppDbContext context = new AppDbContext();

//  context.DataBase.EnsureDelete
context.Database.EnsureCreated();

//create a collection of People (type is Person)
//this list exists (new) but is empty
//this list will be used to hold data returning via a linq query
//  from the database
List<Person> people = new List<Person>();

Console.WriteLine("\n Created an empty instance of the collection");

bool stopFlag = false;
string inputvalue = null;

do
{
    Person individual = GetIndividual();


    //add the individual to our db
    //staging
    //  places the data into a stage in memory which will be 
    //      eventually saved to the database
    //the People in the context class is the DbSet
    //the people in this program class is a List<T> collection
    context.People.Add(individual);

   

    Console.Write("Add another person (Y or N):\t");
    inputvalue = Console.ReadLine();
    if (inputvalue.Trim().ToUpper().Equals("N"))
    {
        stopFlag = true; 
    }
} while (!stopFlag);

//the next step is to commit the data in memory to the physical file (persist)
context.SaveChanges();

//proof of action
//Linq query from the database into our people collection
people = context.People.OrderBy(x => x.Name).ToList();
DisplayDbCollection(people);

/* *************************** Methods ********************************* */
static void DisplayDbCollection(List<Person> people)
{
    Console.WriteLine("\n === Query of Db People ===");
    foreach(var item in people)
    {
        Console.WriteLine($"\t\tName: {item.Name} of age {item.Age} has a mark of {item.Mark}");
    }
}

static Person GetIndividual()
{
    
    string inputValue = "";
    Person person = new Person();
    Console.Write("Enter a name:\t");
    person.Name = Console.ReadLine();
    Console.Write("Enter an age:\t");
    person.Age = int.Parse(Console.ReadLine());
    Console.Write("Enter an Mark:\t");
    person.Mark = int.Parse(Console.ReadLine());

    return person;
}