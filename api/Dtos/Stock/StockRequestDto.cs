using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty; //since this is a string, the def val is null which will throw a null ref error
        [Required]
        [MaxLength(10, ErrorMessage = "Company name cannot be over 10 characters")]

        public string CompanyName { get; set; } = string.Empty; // setting this as string.empty will show that its a string, but just with no value
        [Required]
        [Range(1, 100000)]
        public decimal Purchase { get; set; } // we can rely on mdoel to do the decimal
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; } // decimal is a data type in c# for precise numbers, slower but more accurate than float or double

        [Required]
        [MaxLength(100, ErrorMessage = "Symbol cannot be over 100 characters")]
        public string Industry { get; set; } = string.Empty; //public keyword says it can be accessed anywhere, decimal is the type, the {get; set;} makes it a property (allows you to get and set)
                                                             //you can get by doing myObject.LastDiv (will set and get depending on context used, so basically built in methods)
        [Required]
        [Range(1, 50000000000)]
        public long MarketCap { get; set; }
        //Comments were her
    }
}