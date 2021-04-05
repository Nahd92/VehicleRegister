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
    public class ReservationController : Controller
    {
        private readonly IServiceWrapper _serviceWrapper;
        public ReservationController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route(RoutesAPI.Reservations.GetAllServiceReservations)]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _serviceWrapper.ServiceReservations.GetAllReservations();

            if (reservations == null) return NotFound("Theres no reservations in the list");

            return Ok(reservations);
        }
    }
}
