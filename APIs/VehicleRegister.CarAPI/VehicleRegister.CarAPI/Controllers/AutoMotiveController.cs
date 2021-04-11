using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.RouteAPI;

namespace VehicleRegister.CarAPI.Controllers
{
    [ApiController]
    public class AutoMotiveController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public AutoMotiveController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }


        [HttpGet]
        [Route(RoutesAPI.AutoMotive.GetAllAutoMotives)]
        public async Task<IActionResult> GetAutoMotives()
        {
            var automotives = await _serviceWrapper.RepairService.GetAllAutoMotives();

            if (automotives == null) return NotFound();
           
            return Ok(automotives);
        }

        [HttpGet]
        [Route(RoutesAPI.AutoMotive.GetAutoMotives)]
        public async Task<IActionResult> GetAutoMotive(int id)
        {
            var automotive = await _serviceWrapper.RepairService.GetAutoMotiveById(id);

            if (automotive == null) return NotFound("No AutoMotive could be found");

            return Ok(automotive);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.AutoMotive.CreateAutoMotives)]
        public async Task<IActionResult> AddNewAutoMotivesToDatabase(AddAutoMotiveToListRequest request)
        {
            var response = await _serviceWrapper.RepairService.AddNewAutoMotiveToDatabase(request);

             if (response) 
                  return NoContent();

            return BadRequest("Something happened when trygin to add AutoMotive to list, try again");

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.AutoMotive.UpdateAutoMotive)]
        public async Task<IActionResult> UpdateExistingAutoMotive(UpdateAutoMotiveDto request)
        {
            var response = await _serviceWrapper.RepairService.UpdateAutoMotive(request);

            if (response is null)
               return BadRequest("Something happened when trygin to update AutoMotive, try again");

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.AutoMotive.DeleteAutoMotive)]
        public async Task<IActionResult> DeleteAutoMotive(int id)
        {
            var response = await _serviceWrapper.RepairService.DeleteAutoMotive(id);

            if (response)
                return NoContent();

            return BadRequest("Something happended when trying to delete autoMotive");
        }

    }
}