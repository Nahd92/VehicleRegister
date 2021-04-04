using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.RouteAPI;

namespace VehicleRegister.CarAPI.Controllers
{
    [ApiController]
    public class VehicleController : Controller
    {

        private readonly IServiceWrapper _serviceWrapper;
        public VehicleController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }


        [HttpGet]
        [Route(RoutesAPI.Vehicle.GetAllVehicles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _serviceWrapper.Vehicle.GetAllVehicles();

            if (vehicles == null) return NotFound();

            return Ok(vehicles);
        }


        [HttpGet]
        [Route(RoutesAPI.Vehicle.GetVehicle)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var response = await _serviceWrapper.Vehicle.GetVehicleById(id);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpPost]
        [Route(RoutesAPI.Vehicle.CreateVehicle)]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest request)
        {
            var response = await _serviceWrapper.Vehicle.CreateVehicle(request);

            if (response)
                 return NoContent();

            return BadRequest("Something happened while trying to create a new Vehicle, try again!");
        }


        [HttpDelete]
        [Route(RoutesAPI.Vehicle.DeleteVehicle)]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var response = await _serviceWrapper.Vehicle.DeleteVehicle(id);

            if (response)
                return NoContent();

            return BadRequest("Something happend when trying to delete vehicle! Try again");
        }
    }
}
