using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Response
{
    public class UpdatedReservationResponse
    { 
        public int Id { get; set; }
        public string RegisterNumber { get; set; }
        public int VehicleId { get; set; }
        public string AutoMotiveName { get; set; }
        public DateTime Date { get; set; }
    }
}
