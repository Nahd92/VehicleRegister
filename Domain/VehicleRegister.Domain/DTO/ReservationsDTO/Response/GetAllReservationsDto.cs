using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Response
{
    public class GetAllReservationsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string VehiclesRegisterNumber { get; set; }
        public string AutoMotivesName { get; set; }
    }
}
