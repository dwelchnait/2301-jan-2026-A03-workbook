using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDemos
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
        //declare a property which will represent our collection from the persisted datastore
        //each collection requires it's own property
        public DbSet<Person> People { get; set; }

        //configure the application so that it knows where to locate the datastore
        //BY DEFAULT: the expected location of the datastore will be in the same folder
        //              as your application .exe
        //you can alter the location by using AppDomain.CurrentDomain.BaseDirectory and
        //  relative address

        // Force SQLite to use a single database file in the project root.
        // Prevents "no such table" errors caused by different working
        // directories between `dotnet run` and Visual Studio.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "dpPeople.db");

            //get the full path name from the top of your machine
            dbPath = Path.GetFullPath(dbPath);

            //tell SQLite to use this file
            options.UseSqlite(dbPath);
        }

        //describe to the application the declaration of the table in SQLite
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

                //you can add validation to your properties within the modeling
                //  as well as your validation in your entity class
                entity.ToTable(v => v.HasCheckConstraint("CK_Person_Age", "[Age] >= 0 and [Age] <= 120"));
                entity.ToTable(v => v.HasCheckConstraint("CK_Person_Mark", "[Mark] >= 0 and [Mark] <= 100"));
            }
            );

        }
    }
}
