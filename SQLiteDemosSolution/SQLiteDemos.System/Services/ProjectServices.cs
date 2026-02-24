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
    public class ProjectServices
    {
        #region Connection setup and constructor
        // receive the context connection from the Program.cs
        // save the context connection so that it can be used 
        //      by any service (aka method) within this class

        private readonly AppDbContext _context;
        public ProjectServices(AppDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Queries
        //a service to return a list of projects from the datastore


        public async Task<List<Project>> Project_GetAll()
        {
            //this query is going to the database
            //this service will wait and not tie up the database until it can be handled
            return await _context.Projects
                            .OrderBy(p => p.ProjectName).ToListAsync();

        }
        #endregion
        #region Maintain DataStore (Add, Update and Delete)
        public async Task Project_Add(Project project)
        {
            //Why does Task NOT have a datatype specified like the query
            //This service DOES NOT return any value

            //Guard Rail
            ArgumentNullException.ThrowIfNull(project,nameof(project));

            //Stage

            await _context.Projects
                        .AddAsync(project);

            //for SaveChanges you will want to use await/async
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
