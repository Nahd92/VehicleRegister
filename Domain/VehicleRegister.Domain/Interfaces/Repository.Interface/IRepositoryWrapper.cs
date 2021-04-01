using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Repository.Interface
{
   public interface IRepositoryWrapper
    {
        IVehicleRepository VehicleRepo { get; }
        IAutoMotiveRepairRepository RepairRepo { get; }
        IServiceReservationsRepository ServiceRepo { get; }
    }
}
