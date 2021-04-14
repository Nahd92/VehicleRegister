using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Response
{
    public class VehicleServiceCompletedDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public string VehiclesRegisterNumber { get; set; }
        public string AutoMotivesName { get; set; }
    }
}
