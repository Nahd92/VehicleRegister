using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Request
{
   public class ClientCreateReservationDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public int VehicleId { get; set;}
        [Required]
        public int AutoMotiveRepairId { get; set; }
    }
}
