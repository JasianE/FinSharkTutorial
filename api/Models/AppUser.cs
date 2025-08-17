using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;
namespace api.Models
{
    public class AppUser : IdentityUser // the password and user will be tucked away behind the scene
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>(); // when we establish a many to many, we want a ref to the join table
    }
}