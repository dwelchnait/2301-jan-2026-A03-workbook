using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace SQLiteDemos.System.Models
{
    //this entity will be in a M:M relationship with Project 
    //this entity will be in a 1:M relationship with Department 
    public class Person
    {
        //Id is the "pkey" of the entity that will be SQLite datastore
        //this will be a generated field by SQLite
        public int Id { get; private set; }

        //we can add simple validation to a property
        //this validation can replace the validation within a property allowing for
        //  simple auto-implement coding
        //all annotation for a property EXISTS BEFORE the property
        //to use annotation you will need to add a namespace at the top of your class

        //NOTE: as a standard for our class, you WILL place a custom message on any annotation

        //NOTE: even though you have placed annotation validation on your properties
        //          you could STILL place coded validation within your properties
        //      HOWEVER: you would need to be consistent with the validation (that is, 
        //          they MUST match)

        [Required(ErrorMessage = "Name is required. Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Name is limited to 100 characters.")]
        public string? Name { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Age must be a whole number greater than 0. eg: 5")]
        public int Age { get; set; }

        [Range(0, 100, ErrorMessage = "Mark must be a whole number between 0 and 100. eg: 65")]
        public int Mark { get; set; }

        //constructors are optional with our setup
        public Person() { }
        public Person(string name,  int age, int mark, int departmentid)
        {
            Name=name;
            Age=age;
            Mark=mark;
            DepartmentId = departmentid;
        }

        //used for the 1 to many relationship between Department and Person
        public int DepartmentId { get; set; }

        //Navigational property
        //these property are used to allow for inclusion of data from
        //  other tables in a relational fashion such as 1:m or many to many
        //depending on your software and project type this code may be different

        //when you create your property, you refer to the entity that is
        //  on the other side of your relationship

        //the relationship between Department and Person is 1:M
        //NOTE: the datatype to a 1 relationship is an instance
        public Department Department { get; set; }

        //this is a M:M relationship between Person and Project
        //NOTE: the datatype to a M relationship is a collection (List<T>)
        public List<Project> Projects { get; set; } = new(); //data default to the variable/property datatype
    }
}
