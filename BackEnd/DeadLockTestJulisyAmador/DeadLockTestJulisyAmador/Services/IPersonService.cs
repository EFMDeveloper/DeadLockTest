using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeadLockTestJulisyAmador.Services
{
    public interface IPersonService
    {
        Task<bool> Delete(int Id);
        Task<List<PersonViewModel>> GetAll();
        Task<Person> GetById(int Id);
        Task<bool> Save(Person model);
    }
}