using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.VehicleDTO.Request
{
   public class UpdateVehicleRequest
    {
        public int Id { get; set; }
        public bool IsDrivingBan { get; set; }
        public bool IsServiceBooked { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}
