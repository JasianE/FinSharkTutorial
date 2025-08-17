using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

//This is the schema for storing stocks
namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty; //since this is a string, the def val is null which will throw a null ref error
        public string CompanyName { get; set; } = string.Empty; // setting this as string.empty will show that its a string, but just with no value

        [Column(TypeName = "decimal(18,2)")] // limits sql database to 18 digits and 2 decimal points
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")] // an attribute which is metadata to class, written in square brackets, in this case used by entity framework used for ORM databases
        public decimal LastDiv { get; set; } // decimal is a data type in c# for precise numbers, slower but more accurate than float or double
        public string Industry { get; set; } = string.Empty; //public keyword says it can be accessed anywhere, decimal is the type, the {get; set;} makes it a property (allows you to get and set)
        //you can get by doing myObject.LastDiv (will set and get depending on context used, so basically built in methods)
        public long MarketCap { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>(); // uses the comment class, we want to link everything here
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}