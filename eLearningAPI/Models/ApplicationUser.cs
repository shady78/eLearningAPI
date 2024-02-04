using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eLearningAPI.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
        public int? PasswordResetPin { get; set; } = null;
        public DateTime? ResetExpires { get; set; } = null;
        //[Required]
        //[RegularExpression(@"^[0-9]{14}$", ErrorMessage = "Invalid National Number.")]
        //public string NationalNum { get; set; }
        //public string Photo { get; set; }
        //public string ImagOfCard { get; set; }
    }
}
