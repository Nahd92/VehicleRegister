using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<bool> CreateVehicle(CreateVehicleRequest vehicle) => await _repo.VehicleRepo.CreateVehicle(vehicle);
        

        public async Task<IEnumerable<IVehicle>> GetAllVehicles() => await _repo.VehicleRepo.GetAllVehicles();



    }
}
