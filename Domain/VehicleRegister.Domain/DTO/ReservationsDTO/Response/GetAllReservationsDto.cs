using System;
using System.Collections.Generic;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.ReservationsDTO.Response
{
    public class GetAllReservationsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public string VehiclesRegisterNumber { get; set; }
        public string AutoMotivesName { get; set; }
        public VehicleServiceHistory History { get; set; }

        public GetAllReservationsDto()
        {

        }
        public GetAllReservationsDto(VehicleServiceHistory history)
        {
            History = history;
        }
    }
}
