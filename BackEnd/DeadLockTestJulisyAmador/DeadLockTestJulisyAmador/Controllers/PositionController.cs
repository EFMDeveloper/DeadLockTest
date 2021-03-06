﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadLockTestJulisyAmador.Models;
using DeadLockTestJulisyAmador.Services;
using DeadLockTestJulisyAmador.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeadLockTestJulisyAmador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _Service;

        public PositionController(IPositionService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        
        {
            var result = await _Service.GetAll();
            return
                Ok(result);
        }

        [HttpGet("GetPosition/{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id no econtrado");

            var result = await _Service.GetById(Id);
            return
                Ok(result);
        }


        [HttpPost("SetPosition")]
        public async Task<IActionResult> SetPosition(PositionViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Description))
                    return
                        BadRequest("Debe indicar el nombre de la posición");
                               

                var item = new Position
                {
                    Id= model.Id,
                    Description = model.Description
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

        [HttpGet("Delete/{id}")]
        public async Task<ActionResult> DeletePosition(int Id)
        {
            if (Id <= 0)
                return BadRequest("Id no econtrado");

            var result =await _Service.Delete(Id);
            return
                Ok(result);
        }
    }
}
