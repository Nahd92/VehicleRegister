using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllVehicles() => Ok(await _serviceWrapper.Vehicle.GetAllVehicles());

    }
}
