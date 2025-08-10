using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDTO
    {
        public string Symbol { get; set; } = string.Empty; //since this is a string, the def val is null which will throw a null ref error
        public string CompanyName { get; set; } = string.Empty; // setting this as string.empty will show that its a string, but just with no value

        public decimal Purchase { get; set; } // we can rely on mdoel to do the decimal
        public decimal LastDiv { get; set; } // decimal is a data type in c# for precise numbers, slower but more accurate than float or double
        public string Industry { get; set; } = string.Empty; //public keyword says it can be accessed anywhere, decimal is the type, the {get; set;} makes it a property (allows you to get and set)
        //you can get by doing myObject.LastDiv (will set and get depending on context used, so basically built in methods)
        public long MarketCap { get; set; }
        //Comments were her
    }
}