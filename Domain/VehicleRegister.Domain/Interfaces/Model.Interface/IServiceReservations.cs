using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IServiceReservations
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        int VehicleId {get; set;}
        int AutoMotiveRepairId { get; set; }
    }
}
