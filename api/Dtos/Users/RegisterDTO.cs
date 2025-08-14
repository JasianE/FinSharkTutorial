using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Users
{
    public class RegisterDTO
    {
        [Required]
        public string? Username { get; set; } //nullable
        [Required]
        [EmailAddress] // automatically checks validation for email (dotnet provided)
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; } // alot of validation will be done from the create async method
    }
}