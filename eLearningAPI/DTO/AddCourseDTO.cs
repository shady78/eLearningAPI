using System.ComponentModel.DataAnnotations;

namespace eLearningAPI.DTO
{
    public class AddCourseDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Class_Id { get; set; }
    }
}
