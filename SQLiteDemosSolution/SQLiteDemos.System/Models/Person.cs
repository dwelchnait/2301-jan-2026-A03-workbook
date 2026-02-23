using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SQLiteDemos.System.Models
{
    public class Person
    {
        public int Id { get; set; } //map as PKEY, auto generated

        //your attributes
        //auto or fully implemented
        [Required(ErrorMessage = "Name is a required field. Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Name is limited to 100 characters.")]
        public string Name { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "Age must be a positive number that is 0 or greater.")]
        public int Age { get; set; }

        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100.")]
        public int Mark { get; set; }

        //used for the 1 to many relationship between Department and Person
        // add a property that would contain the value of the parent instance
        public int DepartmentId { get; set; }
        //Navigational property

        //1 to many relationship  One Department has many People
        //Notice on the many declaration this nav property is a single instance
        public Department Department { get; set; }

        //many to many relationship with Person (people)
        //many side, collection type property
        public List<Project> Projects { get; set; } = new();

    }
}
