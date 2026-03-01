
using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;    //needed for the Async methods
using SQLiteDemos.System.DAL;           //needed for your AppDbContent
using SQLiteDemos.System.Helpers;       //contains a service method to active validation annotation
using SQLiteDemos.System.Models;        //needed for your entities
#endregion

namespace SQLiteDemos.System.Services
{
    public class DepartmentServices
    {
        #region context connection and constructor setup
        private readonly AppDBContext _context;

        public DepartmentServices(AppDBContext context)
        {
            _context = context;
        }
        #endregion

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
        //   Prefer async
        //   ReturnDataType Task/Task<T>
        //   Use await SaveChangesAsync() and queries

        //It future-proofs your architecture.
        //That’s clean and industry-standard.

        /* ***************** You are already thinking at a professional architecture level. *********** */

        #region Manipulate Data (Add, Update and Delete)

        //Add a department to the database
        //you could pass in each pieces of data as a separate parameter
        //you could pass in an instance of the class as the parameter
        //Task is used when nothing is returned
        public async Task Department_Add(Department department)
        {
            //Guard Rail
            //ensures that you have data to use with
            //basically validate that you have data to use
            ArgumentNullException.ThrowIfNull(department, nameof(department));

            //Validation is a business rule concern, not a database concern
            //Depending on your application, annotation validation may or maynot be 
            //      automatically activated: for console apps, it is not
            //This call will cause the annotation validation to be activated
            //Why place before staging:
            //  if the object is invalid:
            //      Don't track it
            //      Don't stage it
            //      Don't even touch the DbContext
            //
            //Your validator helper class is a static class, therefore
            //  use the class technique for static classes:  classname.methodname(...)
            //      where ... is your parameter
            ValidatorHelper.Validate(department);

            //Stage the add
            //remember this is done in memory and NOT to the database
            //  therefore this does NOT need to use await/async
            //  BUT there exists Async staging command if you wish to use them
            _context.Departments.Add(department);
            //could also stage using the following
            //await _context.Departments.AddAsync(department);

            //Commit to database
            //this will touch the database
            //this will use await/async
            await _context.SaveChangesAsync();
        }
       

        public async Task Department_Delete(int departmentId)
        {
            //only the Department inastance Id is needed for this service
            //you could pass in the entire instance of department
            //  and extract the Id from the instance
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
                throw new KeyNotFoundException("Department not found.");

            //using Any or All does not load actual records from db to memory
            //simply returns a true or false
           
            bool hasPeople = await _context.People
                                            .AnyAsync(p => p.DepartmentId == departmentId);
            if (hasPeople)
                throw new ArgumentException("Department has assigned people.");

            //validation is not needed here as the record is being removed
            //  from the database

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();
        }

        #endregion
        #region Queries

        //return all departments ordered by department name
        //A query is a service
        //A service is a method
        public async Task<List<Department>> Department_GetAll()
        {
            //the return of the query is a collection : List<Department>
            //can be coded on one physical line but I personally like to code as a list
            return await _context.Departments
                                .OrderBy(d => d.Code)
                                .ToListAsync();
        }
        #endregion
    }
}
