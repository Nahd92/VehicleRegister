using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IServiceReservations
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        bool IsCompleted { get; set; }
         int VehicleId { get; set; }
         Vehicle Vehicle { get; set; }
         int AutoMotiveRepairId { get; set; }
         AutoMotiveRepair AutoMotiveRepair { get; set; }
    }
}
