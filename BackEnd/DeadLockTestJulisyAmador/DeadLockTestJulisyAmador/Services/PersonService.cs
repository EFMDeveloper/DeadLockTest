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
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int Id)
        {
            var result = false;
            var ItemToDelete = await _context.Persons.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Persons.Remove(ItemToDelete);
            _context.SaveChanges();
            result = true;
            return
                result;
        }

        public async Task<List<PersonViewModel>> GetAll()
        {
            var data = await _context.Persons.Include(o => o.Position)
                .Select(x => new PersonViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    PositionId = x.PositionId,
                    PositionDesc = x.Position.Description
                }).ToListAsync();

            return
                data;
        }

        public async Task<Person> GetById(int Id)
        {
            var result = await _context.Persons.FirstOrDefaultAsync(x => x.Id == Id);
            return
                result;
        }

        public async Task<bool> Save(Person model)
        {
            var result = false;
            if(model.Id > 0)
            {
                var datos = await _context.Persons.FirstOrDefaultAsync(x => x.Id == model.Id);
                datos.FirstName = model.FirstName;
                datos.LastName = model.LastName;
                datos.PhoneNumber = model.PhoneNumber;
                datos.PositionId = model.PositionId;
            }
            else
                _context.Persons.Add(model);
            
            
            _context.SaveChanges();
            result = true;
            return
                result;
        }

        public async Task<List<PositionViewModel>> GetPositions()
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
    }
}
