using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace SQLiteDemos.System.Models
{
    //this entity will be in a M:M relationship with Person (People)
    public class Project
    {
        public int Id { get; private set; }

        [Required(ErrorMessage = "Code is required. Code cannot be empty.")]
        [StringLength(10, ErrorMessage = "Code is limited to 10 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Project Name is required. Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Project Name is limited to 100 characters.")]
        public string ProjectName { get; set; }

        //Navigational property
        //this is a M:M relationship between Project and Person
        //NOTE: the datatype to a M relationship is a collection (List<T>)
        public List<Person> People { get; set; } = new(); //data default to the variable/property datatype
    }
}
