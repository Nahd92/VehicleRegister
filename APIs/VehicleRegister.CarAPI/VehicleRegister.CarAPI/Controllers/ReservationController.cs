using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
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
        [Authorize]
        [Route(RoutesAPI.Reservations.GetServiceReservation)]
        public async Task<IActionResult> GetServiceReservation(int id)
        {
            var reservation = await _serviceWrapper.ServiceReservations.GetReservation(id);

            if (reservation == null) return NotFound("No reservaiton exist with that Id");

           return Ok(reservation);
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Reservations.GetAllServiceReservations)]
        public async Task<IActionResult> GetAllServiceReservations()
        {
            var reservations = await _serviceWrapper.ServiceReservations.GetAllReservations();

            if (reservations == null) return NotFound("Theres no reservations in the list");

            return Ok(reservations);
        }




        [HttpPost]
        [Authorize]
        [Route(RoutesAPI.Reservations.BookServiceReservation)]
        public async Task<IActionResult> BookServiceReservation(CreateReservationRequest request)
        {
            var reservation = await _serviceWrapper.ServiceReservations.BookService(request);

            if (reservation)
                return NoContent();

            return BadRequest("Something bad happende, try again");
        }




        [HttpPut]
        [Authorize]
        [Route(RoutesAPI.Reservations.UpdateServiceReservation)]
        public async Task<IActionResult> UpdateServiceReservation(UpdateReservationRequest request)
        {
            var updatedReservation = await _serviceWrapper.ServiceReservations.UpdateServiceReservation(request);

            if (updatedReservation == null) return NotFound("Therese no reservation what inputed ID");

            return Ok(updatedReservation); 
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Reservations.RemoveServiceReservation)]
        public async Task<IActionResult> DeleteServiceReservation(int id)
        {
            var result = await _serviceWrapper.ServiceReservations.DeleteReservation(id);

            if (result)
                return NoContent();

            return BadRequest("Something bad happned, could not delete reservation, try again!");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(RoutesAPI.Reservations.RemoveServiceReservations)]
        public async Task<IActionResult> DeleteServiceReservations()
        {
            var result = await _serviceWrapper.ServiceReservations.DeleteReservations();

            if (result)
                return NoContent();

            return BadRequest("SOmething happned, could not delete reservations, try again");
        }
    }
}
