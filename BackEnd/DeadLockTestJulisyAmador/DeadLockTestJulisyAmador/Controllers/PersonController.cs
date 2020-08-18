using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DeadLockTestJulisyAmador.Services;

namespace DeadLockTestJulisyAmador.Controllers
{

    [ApiController]
    [Route("api/[controller]")]    
    public class PersonController : ControllerBase
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

        [HttpGet("GetDataModel")]
        public async Task<IActionResult> GetPersonModel()
        {
            var result = new PersonViewModel();

            result.Positions = await _Service.GetPositions();
            return Ok(result);
        }

        [HttpPost("SetPerson")]
        public async Task<IActionResult> SetPerson(PersonViewModel model)
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
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PositionId = model.PositionId
                };

                var result = await _Service.Save(item);

                return
                    Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Delete/{id}")]
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
