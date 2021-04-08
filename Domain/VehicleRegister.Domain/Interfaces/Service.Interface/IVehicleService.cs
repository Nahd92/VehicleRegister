using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IVehicleService
    {
        Task<IEnumerable<IVehicle>> GetAllVehicles();
        Task<bool> CreateVehicle(CreateVehicleRequest vehicle);
        Task<IVehicle> GetVehicleById(int id);
        Task<bool> DeleteVehicle(int id);
        Task<UpdateVehicleResponse> UpdateVehicle(UpdateVehicleRequest vehicle);
        Task<IVehicle> GetVehicleWithRegNumber(string regNumber);
    }
}
