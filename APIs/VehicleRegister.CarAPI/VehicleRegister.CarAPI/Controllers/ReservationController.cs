using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public ReservationController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Reservations.GetAllServiceReservations)]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _serviceWrapper.ServiceReservations.GetAllReservations();

            if (reservations == null) return NotFound("Theres no reservations in the list");

            return Ok(reservations);
        }
    }
}
