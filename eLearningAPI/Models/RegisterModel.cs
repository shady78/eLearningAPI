﻿using System.ComponentModel.DataAnnotations;

namespace eLearningAPI.Models
{
    public class RegisterModel
    {
        [Required, StringLength(200)]
        public string FullName { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(128)]
        public string Email { get; set; }
        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}
