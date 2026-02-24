using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using SQLiteDemos.System.DAL;
using SQLiteDemos.System.Models;
using Microsoft.EntityFrameworkCore; //needed for async methods
#endregion

namespace SQLiteDemos.System.Services
{
    public class PersonServices
    {
        #region Connection setup and constructor
        // receive the context connection from the Program.cs
        // save the context connection so that it can be used 
        //      by any service (aka method) within this class

        private readonly AppDbContext _context;
        public PersonServices(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        //a service to return a list of people from the datastore

        
        public async Task<List<Person>> Person_GetAll()
        {
            //this query is going to the database
            //this service will wait and not tie up the database until it can be handled
            return await _context.People
                            .OrderBy(p => p.Name).ToListAsync();

        }
        #endregion

        #region Maintain DataStore (Add, Update and Delete)
        public async Task Person_Add(Person person)
        {
            //Why does Task NOT have a datatype specified like the query
            //This service DOES NOT return any value

            //Guard Rail
            ArgumentNullException.ThrowIfNull(person,nameof(person));

            //Person needs to have a department
            //person is a 1 to many relationship with department

            //Stage
            
            await _context.People
                        .AddAsync(person);

            //for SaveChanges you will want to use await/async
            await _context.SaveChangesAsync();
        }
        public async Task Person_AssignPersonToProject(int personid, List<string> projectCodes)
        {
            //Guard Rail
            ArgumentNullException.ThrowIfNull(projectCodes, nameof(projectCodes));

            if (personid < 1)
                throw new ArgumentException("Person id is invalid", "Person");

            //get all projects the person already belongs to
            //the Include obtains all projects for an individual that exists on the database
            //the Include could be empty
            var person = await _context.People
                                    .Include(p => p.Projects)   //load existing M:M links
                                    .FirstOrDefaultAsync(p => p.Id == personid);

            //check to see if the person actually exists
            if (person == null)
                throw new ArgumentException($"Person of id {personid} does not exist on file", "Person");

            //get the list of projects that match the incoming project codes
            var projects = await _context.Projects
                                        .Where(p => projectCodes.Contains(p.Code)).ToListAsync();

            //loop through all project associated with the projectcodes
            foreach (var project in projects)
            {
                //check if the person is already attached to project
                //  if not add person to project
                //no validation of entities is needed as they already exists (thus valid)
                //  only creating a link on database between two instances
                if (!person.Projects.Contains(project))
                {
                    //use the Navigational Property for the association between Person
                    //   and Project that exists in the Person entity
                    // this will stage 1  or more records for saving
                    person.Projects.Add(project);
                }
            }

            await _context.SaveChangesAsync();

        }
        #endregion
    }
}
