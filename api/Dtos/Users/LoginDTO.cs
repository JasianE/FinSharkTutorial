using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Users
{
    public class LoginDTO
    {
        [Required] // some data annotations for checking complex types
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}