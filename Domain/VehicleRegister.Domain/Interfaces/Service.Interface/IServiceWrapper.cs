using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IServiceWrapper
    {
        IVehicleService Vehicle { get; }
    }
}
