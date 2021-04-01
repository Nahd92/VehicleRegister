using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Models
{
    public class ServiceReservations : IServiceReservations
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public int AutoMotiveRepairId { get; set; }
    }
}
