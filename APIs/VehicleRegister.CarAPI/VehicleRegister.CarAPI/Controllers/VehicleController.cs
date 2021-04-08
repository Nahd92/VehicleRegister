using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.RouteAPI;

namespace VehicleRegister.CarAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class VehicleController : Controller
    {

        private readonly IServiceWrapper _serviceWrapper;
        public VehicleController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }



        [HttpGet]
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Vehicle.GetVehicleWithRegNumber)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleWithRegNumber(string regNumber)
        {
            var vehicles = await _serviceWrapper.Vehicle.GetVehicleWithRegNumber(regNumber);

            if (vehicles == null) return NotFound();

            return Ok(vehicles);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Vehicle.CreateVehicle)]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest request)
        {
            var response = await _serviceWrapper.Vehicle.CreateVehicle(request);

            if (response)
                 return NoContent();

            return BadRequest("Something happened while trying to create a new Vehicle, try again!");
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Vehicle.DeleteVehicle)]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var response = await _serviceWrapper.Vehicle.DeleteVehicle(id);

            if (response)
                return NoContent();

            return BadRequest("Something happend when trying to delete vehicle! Try again");
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Vehicle.UpdateVehicle)]
        public async Task<IActionResult> UpdateVehicle(UpdateVehicleRequest request)
        {
            var response = await _serviceWrapper.Vehicle.UpdateVehicle(request);

            if (response == null) return NotFound("Could not find any Vehicles with inputed Id");

            return Ok(response);
        }


        
    }
}
