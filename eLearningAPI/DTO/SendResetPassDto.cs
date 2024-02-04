using System.ComponentModel.DataAnnotations;

namespace eLearningAPI.DTO
{
    public class SendResetPassDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
