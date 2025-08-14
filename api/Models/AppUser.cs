using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;
namespace api.Models
{
    public class AppUser : IdentityUser // the password and user will be tucked away behind the scene
    {
        
    }
}