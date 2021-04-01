using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Repository.Interface
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<IVehicle>> GetAllVehicles();
        void Create();
    }
}
