using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
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
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _serviceWrapper.Vehicle.GetAllVehicles();

            if (vehicles == null) return NotFound();

            return Json(vehicles);
        }

        [HttpPost]
        [Route(RoutesAPI.Vehicle.CreateVehicle)]
        public async Task<IActionResult> CreateVehicle(CreateVehicleRequest request)
        {
            var response = await _serviceWrapper.Vehicle.CreateVehicle(request);

            if (response)
                 return Ok();

            return BadRequest("Something happened while trying to create a new Vehicle, try again!");
        }
    }
}
