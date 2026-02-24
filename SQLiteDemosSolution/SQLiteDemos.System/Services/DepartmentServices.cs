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
    public class DepartmentServices
    {
        #region Connection setup and constructor
        // receive the context connection from the Program.cs
        // save the context connection so that it can be used 
        //      by any service (aka method) within this class

        private readonly AppDbContext _context;
        public DepartmentServices(AppDbContext context) 
        {
            _context = context;
        }
        #endregion


        #region Queries
        //a service to return a list of departments from the datastore

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
        public async Task<List<Department>> Department_GetAll()
        {
            //this query is going to the database
            //this service will wait and not tie up the database until it can be handled
            return await _context.Departments
                            .OrderBy(d => d.DepartmentName).ToListAsync();
                            
        }
        #endregion
    }
}
