using System.ComponentModel.DataAnnotations;

namespace eLearningAPI.DTO
{
    public class ClassDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
