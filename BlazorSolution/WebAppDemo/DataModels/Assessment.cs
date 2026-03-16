using System.ComponentModel.DataAnnotations;

namespace WebAppDemo.DataModels
{
    //the IValidatableObject is required for the custom validate at the 
    //  botton of this entity
    public class Assessment : IValidatableObject
    {
        [Required(ErrorMessage = "Assessment name is require. Cannot be empty.")]
        [StringLength(15,ErrorMessage ="Assessment name is limited to 15 characters.")]
        public string Name { get; set; }

        [Range(0,100,ErrorMessage ="Assessment weight must be between 0 and 100")]
        public int Weight { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Assessment mark must be greater than 0")]
        public int Mark {  get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Assessment maximum mark must be greater than 0")]
        public int MaxMark { get; set; }

        //read-only calculated field
        //computed value for display only
        //ternary operator 
        //  condition: MaxMark == 0
        //  true value: 0
        //  false value: ((decimal)Mark / (decimal)MaxMark)* (decimal)Weight

        public decimal WeightedMark => MaxMark == 0 ? 0 : ((decimal)Mark / (decimal)MaxMark) * (decimal)Weight;

        
        //show above validation then add "What if we need to compare 2 field together???"
        //NOTE: will not execute if another annotation error exists elsewhere during the validation
        //      order: Property validation
        //              if property validation is good then this executes.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Mark > MaxMark)
            {
                yield return new ValidationResult(
                    "Mark must be less than or equal to MaxMark",
                    new[] { nameof(Mark) }   // attaches error to Mark
                );
            }
        }
    }
}
