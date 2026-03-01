
using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using SQLiteDemos.System.Models;
#endregion

namespace SQLiteDemos.System.DAL
{
    //we will need to go to NuGet Packages and include the following
    // MicroSoft.EntityFrameworkCore

    //your project needs to use software from EF to interface
    //  with your persistent data
    //do obtain the use of the existing EF software we "inherit" a
    //  specific call of EF which is responsible for mapping and transferring
    //  data from your datastore to your program
    //The EF class you need to inherit is called DbContext
    //an inherited class is normally referred to as the "base" class
    public class AppDBContext : DbContext
    {
        //add a constructor that will receive from our UI program
        //  the location of your database
        //Pass this information on the the DbContext base class
        public AppDBContext(DbContextOptions<AppDBContext> options)  : base(options) { }


        //define a container within our application that will represent
        //  the entity of our datastore
        public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        //map the datastore entity to our application class (entity)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //it is recommended that the call to the base (DbContext) OnModelCreating
            //  be retained in your override incase there are alternations
            //  to the DbContext class in the future that could cause concerns
            //  if they are not done
            base.OnModelCreating(modelBuilder);

            //then do user custom work
            //describing your table attributes is optional
            modelBuilder.Entity<Person>(entity =>
            {
                //map
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Age)
                      .IsRequired();

                entity.Property(e => e.Mark)
                     .IsRequired();

                // MANY-TO-MANY CONFIGURATION GOES HERE
                // place in only ONE of the two entities related
                //    either this entity People or Project (below)
                entity.HasMany(d => d.Projects)
                 .WithMany(p => p.People);
            });

            modelBuilder.Entity<Department>(d =>
            {
                d.HasKey(e => e.Id);
                d.HasIndex(e => e.Code).IsUnique();

                // 1-TO-MANY CONFIGURATION GOES HERE
                // indicates many people with one department
                // indicates the class declaration property to use as the foreign Key
                // the onDelete is optional but recommend, prevents deleting a department
                //      that have associated people
                d.HasMany(dep => dep.People)
                 .WithOne(p => p.Department)
                 .HasForeignKey(p => p.DepartmentId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Project>(p =>
            {
                p.HasKey(e => e.Id);
                p.HasIndex(e => e.Code).IsUnique();

                // MANY-TO-MANY CONFIGURATION GOES HERE
                // place in only ONE of the two entities related
                //    either this entity Department or People (above)
                // p.HasMany(d => d.People)
                //       .WithMany(p => p.Projects);
            });
        }
    }
}

