using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Data
{
    public class ApplicationDBContext : DbContext//inherits from dbcontext a class from microsoft entity environment
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) //this creates the database through the entity framework, after looking for the tables below
        : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stock { get; set; } // manipulating the whole table
        public DbSet<Comment> Comments { get; set;} //these are our tables...?
    }
}