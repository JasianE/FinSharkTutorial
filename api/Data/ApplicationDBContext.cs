using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser> // this will be the user class that we are using the identity db context for
    //inherits from dbcontext a class from microsoft entity environment
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) //this creates the database through the entity framework, after looking for the tables below
        : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stock { get; set; } // manipulating the whole table
        public DbSet<Comment> Comments { get; set; } //these are our tables...?
        protected override void OnModelCreating(ModelBuilder builder) // we need to see the db with roles, we need to do a user role and an admin role. 
        {
            base.OnModelCreating(builder); // what does this do?
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "1"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "2"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}