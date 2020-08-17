using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadLockTestJulisyAmador.DataContext;
using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DeadLockTestJulisyAmador.Services;
using Microsoft.AspNetCore.Authorization;

namespace DeadLockTestJulisyAmador.Controllers
{
        
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonService _Service;

        public PersonController(IPersonService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPerson()
        {
            var result = await _Service.GetAll();
            return
                Ok(result);
        }

        [HttpGet("GetPerson/{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id no econtrado");

            var result = await _Service.GetById(Id);

            return
                Ok(result);
        }

        //[HttpGet("GetDataModel")]
        //public IActionResult GetPersonModel()
        //{
        //    //var context = new ApplicationDbContext();
        //    //var result = new PersonViewModel();
        //    //result.Positions = context.Positions.ToList();
        //    return                Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> SetPerson([FromBody] PersonViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                    return
                        BadRequest("Debe indicar el nombre");

                if (string.IsNullOrWhiteSpace(model.LastName))
                    return
                        BadRequest("Debe indicar el apellido");

                if (model.PositionId == 0)
                    return
                        BadRequest("Debe indicar la posicion");

                var item = new Person
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    PositionId = model.PositionId
                };

                var result =await _Service.Save(item);

                return
                    Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> DeletePerson(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id no econtrado");

            var result = await _Service.Delete(Id);
            return
                Ok(result);
        }

    }
}
