using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers //this is an extension method, which is a way to add methods to types without modifying code...
                                     //The first parameter must have the this keyword as this makes it an extension
                                     //So in here were adding a method to the Stock type and we are mapping between the types of stock
                                     // We do this cuz makes code cleaner, organizes logic, adds helper methods to types that i might now own
                                     // Basically a mapper is a method / class that turns an object into a lighter weight object, kinda like object inheritance from jamiesons class. It helps restrict data and is an important concept for restricting data in c#/.net
                                     // You can invoke this w/ stock.ToStockDto()
                                     //Imporatn for security, perforamnce, clean code, and seperation of database and API (the whole reason why we use an api in the first place.)
    {
        public static StockDto ToStockDto(this Stock stockModel) //this is an extension method, like w/ inhertiance in jamieson's cs class, and is used to turn a stock model (all the data) received from the database, into the stockDTO that we define here (lightweight minimized version)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(item => item.ToCommentDTO()).ToList() // we need to turn everything into a dto, then to a list
            }; // create a new stock dto with the following information, basically just a new boiled down version of the stock model returned from the DB
        }

        public static Stock ToStockFromRequestDto(this StockRequestDto stockDto) //reverse of the other mapper, this time were ADDING data
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}