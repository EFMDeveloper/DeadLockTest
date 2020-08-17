using DeadLockTestJulisyAmador.DataContext;
using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int Id)
        {
            var ItemToDelete = await _context.Positions.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Positions.Remove(ItemToDelete);
            _context.SaveChanges();
            return
                true;
        }

        public async Task<List<PositionViewModel>> GetAll()
        {
            var result = new List<PositionViewModel>(0);
            var data = await _context.Positions.ToListAsync();
            result = data.Select(x => new PositionViewModel
            {
                Id = x.Id,
                Description = x.Description
            }).ToList();
            return
                result;
        }

        public async Task<Position> GetById(int Id)
        {
            var result = await _context.Positions.FirstOrDefaultAsync(x => x.Id == Id);
            return
                result;
        }
        public async Task<bool> Save(Position model)
        {
            var result = false;
            await _context.Positions.AddAsync(model);
            await _context.SaveChangesAsync();
            result = true;

            return
                result;
        }
    }
}
