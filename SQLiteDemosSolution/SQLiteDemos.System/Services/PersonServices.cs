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
    }
}
