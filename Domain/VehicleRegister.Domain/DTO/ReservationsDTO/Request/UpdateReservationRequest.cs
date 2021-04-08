using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Request
{
    public class UpdateReservationRequest
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int AutoMotiveId { get; set; }
        public DateTime Date { get; set; }
    }
}
