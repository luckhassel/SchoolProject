using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CourseForCreationDTO:IValidatableObject
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "Max length is 1500")]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult(
                    "The title and description should be different",
                    new[] { "CourseForCreationDTO" });
            }
        }
    }
}
