using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using SQLiteDemos.System.DAL;
using SQLiteDemos.System.Models;
using SQLiteDemos.System.Helpers;
#endregion

namespace SQLiteDemos.System.Services
{
    public class PersonServices
    {
        #region context connection and constructor setup
        private readonly AppDBContext _context;

        //the context connection will be supplied to this service class
        //  from the UI project
        //UI project indicates the datastore to use

        public PersonServices(AppDBContext contextConnection)
        {
            _context = contextConnection;
        }
        #endregion

        #region Manipulation of Data (Add, Delete)

        //Add a person to the database
        //you could pass in each pieces of data as a separate parameter
        //OR
        //you could pass in an instance of the class as the parameter
        //Task is used when nothing is returned
        public async Task Person_Add(Person person)
        {
            ArgumentNullException.ThrowIfNull(person, nameof(person));

            //if FirstOrDefault is not used, then department is IQueryable data set
            //with firstOrDefault used, the department is an instance of a data set
            var department = await _context.Departments
                                    .FirstOrDefaultAsync(x => x.Id == person.DepartmentId);

            if (department == null)
                //note the use of single quotes within double quotes
                throw new ArgumentException($"Department '{person.DepartmentId}' not found.");

            //have the validation annotation of your entity executed
            // using the ValidatorHelper class method
            ValidatorHelper.Validate(person);

            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
        }
       
        public async Task Person_AssignPersonToProject(int personId, List<string> projectCodes)
        {
            // When adding a M:M record here, one only needs to code this method in either
            //   of the M:M services classes (PersonServices OR ProjectServices)
            // In this example, the code has been done in both ONLY as a demonstration
            //The person and project records must exist in their respective tables first

            //get the projects for current person
            //SingleOrDefault is use because only one record is expected to be found
            //  Why? the predicate is using the primary key of the table
            var person = await _context.People
                .Include(p => p.Projects) // load existing M:M links
                .SingleOrDefaultAsync(p => p.Id == personId);  //finds the person record by pkey match

            if (person == null)
                throw new ArgumentException("Person not found.");

            //get the projects that match your parameter projectCodes (projects being added to)
            //codes are used here because they are easier to know then primary key values
            //also, project codes are unique in the database so they act "like" primary keys
            var projects = await _context.Projects
                                    .Where(p => projectCodes.Contains(p.Code))
                                    .ToListAsync();

            //Staging all records to be added to the database
            foreach (var project in projects)
            {
                //check if the person is already attached to project
                //  if not add person to project
                //no validation of entities is needed as they alreay exists (thus valid)
                //  only creating a link on database between two instances
                if (!person.Projects.Contains(project))
                     person.Projects.Add(project);
            }

            //Commit to the database
            await _context.SaveChangesAsync();
        }

        public async Task Person_RemovePersonFromProject(int personId, int projectId)
        {
           // Only primary key values needed
           // When removing a M:M record here, one only needs to code this method in either
           //   of the M:M services classes (PersonServices OR ProjectServices)
           // In this example, the code has been done in both ONLY as a demonstration

            //get the project record
            var project = await _context.Projects
                .Include(p => p.People)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                throw new KeyNotFoundException("Project not found.");

            //get the person record
            var person = project.People
                .FirstOrDefault(p => p.Id == personId);

            if (person == null)
                throw new KeyNotFoundException("Person not assigned to this project.");

            
            //EF will:

            //Remove the row from the hidden join table
            //Leave Person intact
            //Leave Project intact

            //using the project collection obtained above. !!!!!!!!!!!!!!!

            project.People.Remove(person);

            await _context.SaveChangesAsync();
        }

        public async Task Person_Delete(int personId)
        {
            //get person record and any attached projects
            //could also use SingleOrDefaultAsync
            var person = await _context.People
                .Include(p => p.Projects)
                .FirstOrDefaultAsync(p => p.Id == personId);

            //check person actually exists
            if (person == null)
                throw new ArgumentException("Person not found.");

            //does the person have any projects they are still attached 
            //You may have a business rule that states a person must have
            //  no attached project BEFORE being remove
            if (person.Projects.Any())
                throw new ArgumentException("Person is assigned to projects.");

            //EF will:

            //Remove the row from the hidden join table
            //Remove the row from the Person table
            //Leave Project intact

            //NOTICE this is different from the Remove From Project above
            //Above, we are using the project collection in the remove operation
            //HERE
            _context.People.Remove(person);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Queries
        //return all people ordered by Name
        public async Task<List<Person>> Person_GetAll()
        {
            return await _context.People
                                .OrderBy(d => d.Name)
                                .ToListAsync();
        }
        //return all people who do not need to pay into CPP (age > 65)
        public async Task<List<Person>> Person_GetAllOver65()
        {
            //the return of the query is a collection : List<Person>
            return await _context.People
                                .Where(x => x.Age > 65)
                                .OrderBy(x => x.Name)
                                .ToListAsync();
        }

        //return all people AND the department data associated with the person
        //returning the department data is done by the .Include(predicate)
        //  where the predicate is using the appropriate navigation property
        //NOTE: this is child to parent!!!!
        public async Task<List<Person>> Person_GetAllUsingInclude()
        {
            return await _context.People
                .Include(p => p.Department)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<List<Person>> Person_GetAllPeopleAndProjects()
        {
            return await _context.People
                .Include(p => p.Projects)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
        #endregion
    }
}
