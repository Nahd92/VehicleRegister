using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.VehicleDTO.Response
{
    public class GetVehicleDto
    {
        public int Id { get; set; }
        public string RegisterNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Weight { get; set; }
        public DateTime InTraffic { get; set; }
        public bool IsDrivingBan { get; set; }
        public bool IsServiceBooked { get; set; }
        public DateTime ServiceDate { get; set; }
        public int YearlyFee { get; set; }
    }
}
