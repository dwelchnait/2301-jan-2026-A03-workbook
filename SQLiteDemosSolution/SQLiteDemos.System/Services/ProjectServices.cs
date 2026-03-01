using Microsoft.EntityFrameworkCore;
using SQLiteDemos.System.DAL;
using SQLiteDemos.System.Helpers;
using SQLiteDemos.System.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDemos.System.Services
{
    public class ProjectServices
    {
        #region context connection and constructor setup
        private readonly AppDBContext _context;

        public ProjectServices(AppDBContext context)
        {
            _context = context;
        }
        #endregion
        #region Manipulation of Data (Add, Delete)
        public async Task Project_Add(Project project)
        {
            //Guard Rail
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            //have the validation annotation of your entity executed
            // using the ValidatorHelper class method
            ValidatorHelper.Validate(project);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task Project_AssignProjectToPerson(string projectcode, List<int> personIds)
        {
            //get the preople for current project
            //codes are used here because they are easier to know then primary key values
            //also, project codes are unique in the database so they act "like" primary keys
            var project = await _context.Projects
                .Include(p => p.People)
                .SingleOrDefaultAsync(p => p.Code == projectcode);


            if (project == null) 
                throw new ArgumentException("Project not found.");

            var people = await _context.People
                                .Where(p => personIds.Contains(p.Id))
                                .ToListAsync();

            foreach (var person in people)
            {
                if (!project.People.Contains(person))
                    project.People.Add(person);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Project_RemoveProjectFromPerson(int personId, int projectId)
        {
            var person = await _context.People
               .Include(p => p.Projects)
               .FirstOrDefaultAsync(p => p.Id == personId);

            if (person == null)
                throw new KeyNotFoundException("Person not found.");

            var project = person.Projects
                .FirstOrDefault(p => p.Id == projectId);

            if (project == null)
                throw new KeyNotFoundException("Project not assigned to this person.");

            //EF will:

            //Remove the row from the hidden join table
            //Leave Person intact
            //Leave Project intact

            person.Projects.Remove(project);

            await _context.SaveChangesAsync();
        }

        public async Task Project_Delete(Project deleteProject)
        {
            var project = await _context.Projects
                .Include(p => p.People)
                .FirstOrDefaultAsync(p => p.Id == deleteProject.Id);

            if (project == null)
                throw new KeyNotFoundException("Project not found.");

            if (project.People.Any())
                throw new ArgumentException("Project has assigned people.");

            //EF will:

            //Remove the row from the hidden join table
            //Remove the row from the Project table
            //Leave People intact

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Queries
        public async Task<List<Project>> Project_GetAll()
        {
            return await _context.Projects.OrderBy(d => d.Code).ToListAsync();
        }
        public async Task<List<Project>> Project_GetAllProjectsAndPeople()
        {
            return await _context.Projects
                .Include(p => p.People)
                .OrderBy(p => p.Code)
                .ToListAsync();
        }
        #endregion
    }
}
