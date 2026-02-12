using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace SQLiteDemos
{
    public class Department
    {
        public int Id { get; set; }

        //we can add simple validation to a property
        //this validation can replace the validation within a property allowing for
        //  simple auto-implement coding
        //all annotation for a property EXISTS BEFORE the property

        //NOTE: as a standard for out class, you WILL place a custom message on any annotation

        [Required(ErrorMessage ="Code is a required field. Code cannot be empty.")]
        [StringLength(10, ErrorMessage = "Code is limited to 10 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Department Name is a required field. Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Department Name is limited to 100 characters.")]
        public string DepartmentName { get; set; }

        //Navigational property
        public List<Person> People { get; set; } = new();

    }
}
