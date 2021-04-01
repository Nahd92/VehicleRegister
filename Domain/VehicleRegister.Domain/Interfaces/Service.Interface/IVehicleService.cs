﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<IVehicle>> GetAllVehicles();
        Task<bool> CreateVehicle(CreateVehicleRequest vehicle);
    }
}
