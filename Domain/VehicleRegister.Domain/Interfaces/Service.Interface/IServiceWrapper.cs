using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IServiceWrapper
    {
        IVehicleService Vehicle { get; }
        IAutoMotiveRepairService RepairService { get; }
        IServiceReservationService ServiceReservations { get; }
        IAuthenticationService authService { get; }
    }
}
