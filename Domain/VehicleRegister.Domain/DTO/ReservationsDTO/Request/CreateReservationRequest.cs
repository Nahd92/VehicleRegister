using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Request
{
    public class CreateReservationRequest
    {
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int AutoMotiveRepairId { get; set; }

    }
}
