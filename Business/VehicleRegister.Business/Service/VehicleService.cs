using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
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
                Weight = vehicle.Weight,              
                YearlyFee = CalculateYearlyFee(vehicle.Weight)
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

        public int CalculateYearlyFee(int weight)
        {
            switch (weight)
            {
                case int n when(n <= 1800):
                    return 1200;
                case int b when (b > 1800 && b <= 2500):
                    return 1800;
                default:
                    return 4500;
            }
        }

        public async Task<UpdateVehicleResponse> UpdateVehicle(UpdateVehicleRequest request)
        {
            var vehicle = await _repo.VehicleRepo.GetVehicleById(request.Id);

            if (vehicle is null) return null;

            vehicle.IsDrivingBan = request.IsDrivingBan;
            vehicle.IsServiceBooked = request.IsServiceBooked;
            vehicle.ServiceDate = request.ServiceDate;

            await _repo.VehicleRepo.UpdateVehicle(vehicle);


            return new UpdateVehicleResponse
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                IsDrivingBan = vehicle.IsDrivingBan,
                ServiceDate = vehicle.ServiceDate,
                IsServiceBooked = vehicle.IsServiceBooked,
                InTraffic = vehicle.InTraffic,
                Model = vehicle.Model,
                RegisterNumber = vehicle.RegisterNumber,
                Weight = vehicle.Weight,
                YearlyFee = vehicle.YearlyFee
            };
        }

        public async Task<IVehicle> GetVehicleWithRegNumber(string regNumber)
        {
            var vehicle = await _repo.VehicleRepo.GetAllVehicles();

            if (vehicle.Any(x => x.RegisterNumber == regNumber))
                 return vehicle.Where(x => x.RegisterNumber == regNumber).FirstOrDefault();

            return null;          
        }
    }
}
