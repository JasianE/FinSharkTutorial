using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository // ctrl . implements the interface auto
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync(); //auto generates the id for us!

            return stockModel;

        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(item => item.Id == id);

            if (stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            _context.SaveChanges();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable(); // as queryable starts building the sql command
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
            }

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockModel = await _context.Stock.Include(stock => stock.Comments).FirstOrDefaultAsync(item => item.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            return stockModel;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(stock => stock.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO updateDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(item => item.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap; //EF is tracking these changes then updating them to DB directly

            await _context.SaveChangesAsync();

            return stockModel;
        }
        
        
    }
}