using System;
using System.Linq;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using SQLiteDemos;
using SQLiteDemos.System.Services;
using System.Runtime.ConstrainedExecution;
using System.Data;
using SQLiteDemos.System.Models;
using SQLiteDemos.System.DAL;
#endregion

// See https://aka.ms/new-console-template for more information
Console.WriteLine("\n\t\tHello, SQLite World!\n");

//we will need to go to NuGet Packages and include the following
// MicroSoft.EntityFrameworkCore
// MicroSoft.EntityFrameworkCore.Tools
// MicroSoft.EntityFrameworkCore.SQLite

//these packages are add to your project under Dependencies
//this has to be done ONCE for your project

// Force SQLite to use a single database file in the project root.
// Prevents "no such table" errors caused by different working directories
//adjusted path, one needs to know the location of your .exe
//      AppDomain.CurrentDomain.BaseDirectory
//once the .exe location is known, I can combine that location with
//      relative address to reach the desired location of the SQLite file
var dbPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "demo.db");

//however we need to know the location of your application on the drive
//phyiscal absolute address location
dbPath = Path.GetFullPath(dbPath);

//setup the connection string information into a variable that can be 
//      pass to the AppDbContext class
//This allows for the user developing a front end for an existing system
//      indicate their desired database location

var options = new DbContextOptionsBuilder<AppDBContext>()
    .UseSqlite($"Data Source={dbPath}")
    .Options;


//create an instance of the context class to be used by the program
using AppDBContext context = new AppDBContext(options);

//use Dependency Injection to let service classes to know about context to db contection
//Why This Is Better
//Your class library should NOT know about:
//  SQLite
//  File paths
//  Connection strings

//That is infrastructure.
//using this technique loosely couples your service with a context class
// thus your front end can identify your data stores

//The console app is the "composition root" — it wires everything together.

var departmentServices = new DepartmentServices(context);
var peopleServices = new PersonServices(context);
var projectServices = new ProjectServices(context);



//if you wish to start your application with a clean file on every execution
// use the following
//WARNING: if you alter your entities, add a new entity or remove an entity
//          from your database, you MUST drop your current database and
//          recreate the db SQLite file.
context.Database.EnsureDeleted();

//ensure that your database file exists
// if the database does not exist, then the data is created using the context class
//  mapping; and your database will be empty
// if the database does exist, then no action is taken and the existing data
//  remains to be used by your application
context.Database.EnsureCreated();

/* ******************** Modern Industry Standard using async and await ***********************/

//Modern EF Core is async-first
//Most examples in Microsoft docs use async now.

//Consistency
//If your service layer ever moves to:
//  ASP.NET Core
//  Blazor
//  Background services
//  APIs

//Then async is already in place.

//Industry Reality(2026)
//Web/API apps:  Async everywhere

//Rule of Thumb
//If your method touches the database:
//  Prefer async
//  Return Task
//  Use await SaveChangesAsync()

//It future-proofs your architecture.
//That’s clean and industry-standard.

/* ***************** You are already thinking at a professional architecture level. *********** */

Console.WriteLine("\nAdding departments to the database");

//separate the C/S functionality.
// Presentation Layer is responsible to obtain or display data from/to user
// Services (Business Logic Layer BLL) responsible to the functionality of the application
// DbContext (Data Access Layer DAL) responsible for interaction with EntityFramework and Database
// Model Entity Declaration (Data Transfer Objects DTO entitites0 describes each table in db as entity class

//call a service method to process the input data in the system

//assume you have an input module to gather the department data (prompts, readlines, etc.)
Department department = new Department();
department.Code = "SDEV";
department.DepartmentName = "Software Development";

//send the input data to the database to be persisted
//this will be the functionality of the application (class library)
//What services do I need to used? department services
//How do I reference the department services? departmentServices (via Dependency Injection)
//****** since the services are async/task the calls will need to have an await *******
await departmentServices.Department_Add(department);

department = new Department();
department.Code = "WEBD";
department.DepartmentName = "Web Development";
await departmentServices.Department_Add(department);

department = new Department();
department.Code = "GDEV";
department.DepartmentName = "Game Development";
await departmentServices.Department_Add(department);

department = new Department();
department.Code = "Empty";
department.DepartmentName = "Empty Can Delete";
await departmentServices.Department_Add(department);

Console.WriteLine("\nRetrieve departments from the database");
//use Department Linq service to get the data records from the datastore via the context DbSet
List<Department> departmentContents = new List<Department>();
departmentContents = await departmentServices.Department_GetAll();

//display the current contains of People
foreach (var item in departmentContents)
{
    Console.WriteLine($"ID {item.Id} Code: {item.Code} Name: {item.DepartmentName}");
}

Console.WriteLine("\nAdding people to the database");
await peopleServices.Person_Add(new Person("Bob G", 22, 25, 1));
await peopleServices.Person_Add(new Person("Terry B", 52, 47, 1));
await peopleServices.Person_Add(new Person("Don W", 42, 45, 1));
await peopleServices.Person_Add(new Person("Terry C", 32, 47, 2));
await peopleServices.Person_Add(new Person("Don K", 72, 75, 2));
await peopleServices.Person_Add(new Person("Pat T", 53, 65, 3));
await peopleServices.Person_Add(new Person("Dan C", 28, 77, 3));

Console.WriteLine("\nRetrieve people from the database without department name");
//use Person Linq service to get the data records from the datastore via the context DbSet
List<Person> peopleContents = await peopleServices.Person_GetAll();

////display the current contains of People
foreach (var item in peopleContents)
{
    Console.WriteLine($"ID {item.Id} Name: {item.Name} Age: {item.Age} Mark: {item.Mark} Department {item.DepartmentId}");
}

Console.WriteLine("\nRetrieve people from the database with department name");
//use Person Linq service to get the data records from the datastore via the context DbSet
peopleContents = await peopleServices.Person_GetAllUsingInclude();

////display the current contains of People
foreach (var item in peopleContents)
{
    Console.WriteLine($"ID {item.Id} Name: {item.Name} Age: {item.Age} Mark: {item.Mark} Department {item.Department.DepartmentName}");
}

Console.WriteLine("\nAdding project to the database");
await projectServices.Project_Add(new Project() { Code= "PRJ01", ProjectName = "Basic C# Lessons" });
await projectServices.Project_Add(new Project() { Code="PRJ02", ProjectName = "Unit Testing Lessons" });
await projectServices.Project_Add(new Project() { Code="PRJ03", ProjectName = "Linq, Class Library and Unit Testing Lessons" });
await projectServices.Project_Add(new Project() { Code="PRJ04", ProjectName = "Client Server Putting it together" });
await projectServices.Project_Add(new Project() { Code="PRJ05", ProjectName = "WEB Client Server with Blazor" });


Console.WriteLine("\nRetrieve projects from the database");
//use Person Linq service to get the data records from the datastore via the context DbSet
List<Project> projectContents = await projectServices.Project_GetAll();

////display the current contains of People
foreach (var item in projectContents)
{
    Console.WriteLine($"ID {item.Id} Code: {item.Code} Name {item.ProjectName}");
}


Console.WriteLine("\nAdding project and people to the database");
//How Many-to-Many Works in EF Core

//You have:
//Person ↔ Project many-to-many
//EF automatically creates a join table behind the scenes, usually named
//      something like PersonProject (or similar)

//EF Core:

//Inserts a row into the join table linking that Person → Project
//Updates in-memory navigation collections if entities are tracked
//Persists everything to your SQLite database file
//The created file is an index file containing sets of primary keys from each entity
//  Person primary key id of 1, 2, 3
//  Project primary key id of 1,2, 3, 4
//  table
// Person / Project
//   1        2
//   1        3
//   2        1
//   2        2
//   3        1
//   3        3
//   3        4

//Do I need to do addes in both direction people to project and projects to person
//NO!!!!!!!!! do one or the other. EF handles the other for you
//What if I attempt to accidently in both directions?
//  the way the service is coded, if the association already exists, it will be ignored
//  and not cause an error.

List<string> projectCodes = new List<string> {"PRJ01", "PRJ02" };
await peopleServices.Person_AssignPersonToProject(1, projectCodes);
projectCodes = new List<string> { "PRJ02" };
await peopleServices.Person_AssignPersonToProject(5, projectCodes);
projectCodes = new List<string> { "PRJ03", "PRJ04" };
await peopleServices.Person_AssignPersonToProject(4, projectCodes);

List<int> personIds = new List<int> { 1, 3 };
await projectServices.Project_AssignProjectToPerson("PRJ03", personIds);

Console.WriteLine("\nList people and their projects");

//use Person Linq service to get the data records from the datastore via the context DbSet
List<Person> peopleandprojectsContents = await peopleServices.Person_GetAllPeopleAndProjects();

////display the current contains of People
foreach (var item in peopleandprojectsContents)
{
    Console.WriteLine("\n");
    if (item.Projects.Count > 0)
    {
        foreach (var proj in item.Projects)
        {
            Console.WriteLine($"ID {item.Id} Name: {item.Name} assigned to ID {proj.Id} Code: {proj.Code} Name {proj.ProjectName}");
        }
    }
    else
    {
        Console.WriteLine($"ID {item.Id} Name: {item.Name} has not be assigned to a project");
    }

}

Console.WriteLine("\nList Projects and their people");

//use Person Linq service to get the data records from the datastore via the context DbSet
List<Project> projectsandpeopleContents = await projectServices.Project_GetAllProjectsAndPeople();

////display the current contains of People
foreach (var item in projectsandpeopleContents)
{
    Console.WriteLine("\n");
    if (item.People.Count > 0)
    {
        foreach (var person in item.People)
        {
            Console.WriteLine($"Project {item.Code} has person assign to it: ID {person.Id} Name: {person.Name}");
        }
    }
    else
    {
        Console.WriteLine($"ID {item.Id} Code: {item.Code} has not be assigned any person.");
    }

}

//deleting records

Console.WriteLine($"\n\nDeletes");
/********************************* Departments ***********************************/
Console.WriteLine($"\nDepartments");
try
{
    await departmentServices.Department_Delete(1); //has people
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nDelete exception: {ex.Message}"); 
}
try
{
    await departmentServices.Department_Delete(4); //has no people
    Console.WriteLine("\nDepartment Empty has been deleted");
    Console.WriteLine("Attempt to add to Department Empty which has been deleted");
    await peopleServices.Person_Add(new Person("Bad Add", 28, 77, 4));
}
catch (Exception ex)
{
    Console.WriteLine($"\nData Exception: {ex.Message}");
}

/********************************* Person ***********************************/
Console.WriteLine($"\n\nPeople");
try
{
   
    await peopleServices.Person_Delete(77);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nDelete exception: {ex.Message}");
}
try
{

    await peopleServices.Person_Delete(1);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nDelete exception: {ex.Message}");
}
try
{
    await peopleServices.Person_Delete(2); //has no projects
    Console.WriteLine("\nPerson id 2 Terry B has been deleted");
    Console.WriteLine("Attempt to assign person 2 (which has been deleted) to projects ");
    projectCodes = new List<string> { "PRJ03", "PRJ04" };
    await peopleServices.Person_AssignPersonToProject(2, projectCodes);
   
}
catch (Exception ex)
{
    Console.WriteLine($"\nData Exception: {ex.Message}");
}