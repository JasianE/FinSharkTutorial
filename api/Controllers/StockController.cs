using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/stock")] // the name of the controller
    [ApiController] // the attribute ?
    public class StockController : ControllerBase // we dont want the database in the controller it should be in the repository for code cleaniliness
    {
                                                      //This line of code is instantiating a variable _context that is of type ApplicationDBcontext, but is currently empty and is then filled by the context that is injected

        private readonly IStockRepository _stockRepo; //this is the naming convention / styling convention for dependency injections.                                        // We inject because it is a design pattern that results in looser coupling
        public StockController(IStockRepository stockRepo) //constructor for the object
        //Oh i understand it now, i guess this is automatically injecting it from something idek but it is automatically injecting it from reading the file
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto()); // we got stuff out of the database, asyn cadded is we now wait for the yield
                                          //The point of the tolist is to have deferred execution,
            return Ok(stockDto); //creates an ok object with the content we want
        }// the async will make a task, and will then insert the value from the result of the yield
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id); //each method i guess has an async version

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto()); //sends an ok response plus the object we want to return ig in json
        } // this is syntatical sugar i guess, basically whats actually happening is im doing stockmapper.toStockDto(stock) --> makes sense
          // But since i use the this definition in the stockmappers extension method, it automically fills in the stock that im invoking on which does the same thing
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockRequestDto stockDto) // we also need to make a CREATE DTO / RECEIVING DTO since we don't want all the info available for a stock from the user.
        {
            var stockModel = stockDto.ToStockFromRequestDto();
            await _stockRepo.CreateAsync(stockModel); //auto generates the id for us!

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
            //follows rest back pracitces (client can immediately access new resource)y
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto); // return the item with the same id

            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteStock([FromRoute] int id) //
        {
            Stock? stock_model = await _stockRepo.DeleteAsync(id);

            if (stock_model == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}