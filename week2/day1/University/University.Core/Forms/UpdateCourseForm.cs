using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public class UpdateCourseForm
    {
        [Required(ErrorMessage = "Course name is required.")]
        [MinLength(2, ErrorMessage = "Course name must be at least 2 characters.")]
        public string Name { get; set; } = null!;

        [Range(0, 100, ErrorMessage = "Course weight must be between 0 and 100.")]
        public int Weight { get; set; }
    }
}
