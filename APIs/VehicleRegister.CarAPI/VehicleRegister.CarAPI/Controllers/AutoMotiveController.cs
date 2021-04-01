using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAutoMotives() => Ok(await _serviceWrapper.RepairService.GetAllAutoMotives());
    }
}
