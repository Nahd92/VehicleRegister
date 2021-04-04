using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Business.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepositoryWrapper _repo;
        public VehicleService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateVehicle(CreateVehicleRequest vehicle)
        {
            var createVehicle = new Vehicle()
            {
                Brand = vehicle.Brand,
                IsDrivingBan = vehicle.IsDrivingBan,
                ServiceDate = vehicle.ServiceDate,
                IsServiceBooked = vehicle.IsServiceBooked, 
                InTraffic = vehicle.InTraffic,
                Model = vehicle.Model,
                RegisterNumber = vehicle.RegisterNumber,
                Weight = vehicle.RegisterNumber,
                YearlyFee = vehicle.YearlyFee
            };

           return await _repo.VehicleRepo.CreateVehicle(createVehicle);
        }

        public async Task<IEnumerable<IVehicle>> GetAllVehicles() => await _repo.VehicleRepo.GetAllVehicles();
        public async Task<IVehicle> GetVehicleById(int id) => await _repo.VehicleRepo.GetVehicleById(id);

        public async Task<bool> DeleteVehicle(int id)
        {
            var vehicle = await _repo.VehicleRepo.GetVehicleById(id);

            if (vehicle is null) return false;
            
           return await _repo.VehicleRepo.DeleteVehicle(vehicle);
        }
    }
}
