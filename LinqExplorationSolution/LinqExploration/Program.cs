using LearningObjects; //have knowledge of Person class

// See https://aka.ms/new-console-template for more information
Console.WriteLine("\n\tWorld of Exploration on Collections!\n");

//create a collection in C#
//a collection could be any structure that holds more than one occurrence of an item
//  a one time

//array[] will hold a fixed number items
//List<T> will an unknown number of items
//          <T> indicates the datatype of the items being held

List<Person> workers = null; //a variable capable of holding a List instance of datatype Person

//create the List<T> instance
workers = new List<Person>();

//you can do the previous two statements together
//List<Person> workers = new List<Person>();

//at this point the list exists but it is empty
Console.WriteLine($"The list of workers is empty. It exists but the worker count is {workers.Count}.");

//create an instance of your Person class
Person person = new Person("Don", 70, 101500.00m);

//add the instance of person to the collection
workers.Add(person);

//shorten the statement by usde the .Add(new class(....))
workers.Add(new Person("Pat", 32, 45780.00m));
workers.Add(new Person("Evan", 32, 77777.00m));
workers.Add(new Person("Terry", 20, 56678.00m));
workers.Add(new Person("Sam", 60, 89901.00m));

//Remember, a list is flexible
//List can hold zero, 1 or more items of your <T> datatype
Console.WriteLine($"\nWorker list exists and the worker count is {workers.Count}.");

//pass a list<T> to a method as an argument
DisplayCollection(workers);

// --- STEP 1: Filter (Where(predicate)) -------------------------------
// the common form of a predicate
//  instanceplacehoder => instanceplaceholderAttribute operator value
//worker is an instance of a class. Accessing a component in the class uses the dot operator followed
//      by a property, a public data member or a method
//what is returned from the .Where() are instances of the workers collection that pass
//      the predicate condition
//the datatype var is set during run time and will not change
//Linq will return either a IEnumerable or IQueryable dataset
//Within C# programming the dataset will probably be IEnumerable
//you can convert the default IEnumerable<T> to a List<T> using the method .ToList();

//can the predicate condition have logically operators (and/or): yes
//  instanceplacehoder => (instanceplaceholderAttribute operator value &&
//                           instanceplaceholderAttribute operator value) ||
//                           instanceplaceholderAttribute operator value

//what is happening with the following statement
//the collection workers is passed into the .Where method
//the .Where method does it's thing
//the .Where method will return a temporary collection in method
//the returned collection is then passed into the .ToList method
//the .ToList converts the collection into a List<T> dataset
//the converted dataset is return from List<T>
//the dataset is assigned to the receiving variable
var oldWorkers = workers.Where(x => x.Age > 50).ToList(); //example of Linq method syntax
DisplayCollection(oldWorkers);

// --- STEP 2: Order  -------------------------------
//OrderBy(predicate) is ascending, use on first ordering field
//OrderByDescending(predicate) is descending, use on first ordering field
//ThenBy(predcate) is ascending, use for second, third, .... ordering field
//ThenByDescending(predicate) is descending, use for second, third, .... ordering field

//var YoungWorkers = workers.Where(x => x.Age < 51);
//var sortedYoungWorkers = YoungWorkers.OrderBy(x => x.Age);

//combine these statements into one

// the workers collection is past into .Where
//.Where return a resulting collection
//.Where collection is past into .OrderBy
//.OrderBy returns a resulting collection
//.OrderBY collection is assigned to sortedYoungWorkers
var sortedYoungWorkers = workers.Where(x => x.Age < 51)
                                .OrderBy(x => x.Age)
                                .ThenByDescending(x => x.Wage)
                                .ToList();
DisplayCollection(sortedYoungWorkers);

// --- STEP 3: Finding items in a Collection  -------------------------------

//Using the .Where(...) to locate an item
//Where(  ) returns a collection. That collection maybe empty, one record, or more many records
//see Where examples above

//What if you just wish to know if an item is in the collection?
//You does not need to want a returned collection
//The are two boolean filter commands for searching your collection: Any or All
//Any(predicate) will return true if any instance in your collection matching the predicate : return is a boolean
//All(predicate) will return true if all instances in your collection matches the predicate : return is a boolean

//These are very useful in decision making (if(... ) statement)
if (workers.All(x => x.Age <= 65))
    // all instances match the condition within the predicate
    Console.WriteLine("\nThe workers all need to pay into CPP.");
else
    // at least one instances does not match the condition within the predicate
    Console.WriteLine("\nSome workers do not need to pay into CPP.");

if (workers.Any(x => x.Age > 65))
{
    // at least one instances matches the condition within the predicate
    Console.WriteLine($"\nAt least one worker does not need to pay into CPP.");
}
else
{
    // no instances match the condition within the predicate
    Console.WriteLine($"\nThe workers all need to pay into CPP. No old people.");
}

// --- STEP 4: Finding items in a Collection without using .Where ----------------------
//What if you wish to locate an instance in your collection by a unique key value (pkey)
//The solution will possible return an instance or null
//possible solutions:
// You can use Where(...)
// You can also use collection methods such as Find(..), FindAll(...), FindLast(...), FindIndex (...)
// You could also use a Linq method such as First<>([...]) or FirstOrDefault<>([...])
Person foundThem = workers.Find(x => x.Age > 29 &&  x.Age < 60);

if (foundThem != null)
    Console.WriteLine($"\nFound {foundThem.Name} is of age {foundThem.Age}.");
else
    Console.WriteLine($"\nNo workers in that age bracket.");

//CANNOT use Linq method with collection methods
//OrderBy is a Linq method
//Find is a collection method
//Person OrderfoundThem = workers.OrderBy(x => x.Age).Find(x => x.Age > 19 &&  x.Age < 60);

//Linq method
Person LinqfoundThem = workers.FirstOrDefault(x => x.Age > 19 &&  x.Age < 50);
if (LinqfoundThem != null)
    Console.WriteLine($"\nFound {LinqfoundThem.Name} is of age {LinqfoundThem.Age}.");
else
    Console.WriteLine($"\nNo workers in that age bracket.");

Person OrderLinqfoundThem = workers.OrderBy(x => x.Age)
                                    .FirstOrDefault(x => x.Age > 19 &&  x.Age < 50);

if (OrderLinqfoundThem != null)
    Console.WriteLine($"\nFound {OrderLinqfoundThem.Name} is of age {OrderLinqfoundThem.Age}.");
else
    Console.WriteLine($"\nNo workers in that age bracket.");

/* ************************* methods ********************************* */

//Remember to isolate your methods use : static
//variables are now local and NOT global
//WARNING! List is a reference variable type
//          any changes to list will still be present when
//              you return to your driver code
static void DisplayCollection(List<Person> workers)
    {
        //foreach loop will process your collection from the 1st item to the last item
        //foreach is a While loop structure (pre-test loop)
        //"item" placeholder only exists as long as the foreach is executing
        Console.WriteLine($"\nWorker list Collection.\n");
        foreach (var item in workers)
        {
            Console.WriteLine($"The item in the collection is: {item.ToString()}");
        }
    }