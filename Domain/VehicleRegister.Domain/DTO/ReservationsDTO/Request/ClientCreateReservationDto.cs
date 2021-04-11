using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Request
{
   public class ClientCreateReservationDto
    {
        public DateTime Date { get; set; }
        public int VehicleId { get; set;}
        public int AutoMotiveRepairId { get; set; }
    }
}
