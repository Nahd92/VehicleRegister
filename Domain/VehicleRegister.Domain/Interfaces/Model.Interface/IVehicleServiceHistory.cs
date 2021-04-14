using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IVehicleServiceHistory
    {
        int Id { get; set; }
        DateTime ServiceDate { get; set; }
        int VehicleId { get; set; }
        Vehicle Vehicle { get; set; }
        int AutoMotiveRepairId { get; set; }
        AutoMotiveRepair AutoMotiveRepair { get; set; }
    }
}
