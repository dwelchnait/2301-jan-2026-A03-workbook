using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using SQLiteDemos.System.Models;
#endregion

namespace SQLiteDemos.System.DAL
{
    //NOTE: your project  will need to add some NuGet packages
    //      to work with EntityFramework
    //  Microsoft.EntityFrameworkCore
    //  Microsoft.EntityFrameworkCore.Tools
    //  Microsoft.EntityFrameworkCore.SQLite

    //this called needs to inherit EF via the base class DbContext
    //EF DbContext handles all the physical I/O functionality to your persistent datastore

    //this class is often referred to as the DAL class (Data Access Layer)
    public class AppDbContext : DbContext
    {
        //add a constructor that will receive from our UI program
        //  the location of your database
        //Pass this information on the the DbContext base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //declare a property which will represent our collection from the persisted datastore
        //each collection requires it's own property
        public DbSet<Person> People { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Project> Projects { get; set; }

        //describe to the application the declaration of the table in SQLite
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

                entity.HasMany(pr => pr.Projects)
                    .WithMany(p => People);
            });


            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Code).IsUnique();
                // MANY-TO-MANY CONFIGURATION GOES HERE
                // place in only ONE of the two entities related
                //    either this entity Project or People (above)
                // entity.HasMany(d => d.People)
                //       .WithMany(p => p.Projects);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Code).IsUnique();

                // 1-TO-MANY CONFIGURATION GOES HERE
                // indicates many people with one department
                // indicates the class declaration property to use as the foreign Key
                // the onDelete is optional but recommend, prevents deleting a department
                //      that have associated people
                entity.HasMany(dep => dep.People)
                        .WithOne(p => p.Department)
                        .HasForeignKey(p => p.DepartmentId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
