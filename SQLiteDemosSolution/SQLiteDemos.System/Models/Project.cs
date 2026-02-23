using System;
using System.Collections.Generic;
using System.Text;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
#endregion
namespace SQLiteDemos.System.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is a required field. Code cannot be empty.")]
        [StringLength(10, ErrorMessage = "Code is limited to 10 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Project Name is a required field. Name cannot be empty.")]
        [StringLength(100, ErrorMessage = "Project Name is limited to 100 characters.")]
        public string ProjectName { get; set; }

        //Navigational Property
        //many to many relationship with Person (people)
        //many side, collection type property
        public List<Person> People { get; set; } = new();
    }
}
