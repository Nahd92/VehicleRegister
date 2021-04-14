using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Repository.Interface
{
    public interface IVehicleServiceHistoryRepository
    {
        Task<bool> AddOldServicesToHistory(List<IVehicleServiceHistory> oldServices);
        Task<bool> AddOldServiceToHistory(IVehicleServiceHistory oldService);
        Task<IEnumerable<IVehicleServiceHistory>> VehicleHistory();
    }
}
