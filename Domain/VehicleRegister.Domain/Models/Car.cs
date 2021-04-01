using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Models
{
    public class Car : IVehicle
    {
        [Key]
        public int Id { get; set; }
        public int RegisterNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Weight { get; set; }
        public DateTime InTraffic { get; set; }
        public bool DrivingBan { get; set; }
        public bool ServiceBooked { get; set; }
        public DateTime ServiceDate { get; set; }
        public int YearlyFee { get; set; }
    }
}
