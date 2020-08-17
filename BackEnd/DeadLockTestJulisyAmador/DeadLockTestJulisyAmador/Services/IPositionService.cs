using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.Services
{
    public interface IPositionService
    {
        Task<bool> Delete(int Id);
        Task<List<PositionViewModel>> GetAll();
        Task<Position> GetById(int Id);
        Task<bool> Save(Position model);
    }
}