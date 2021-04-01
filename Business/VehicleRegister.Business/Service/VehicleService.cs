using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;

namespace VehicleRegister.Business.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepositoryWrapper _repo;
        public VehicleService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }


        public async Task<IEnumerable<IVehicle>> GetAllVehicles() => await _repo.VehicleRepo.GetAllVehicles();



    }
}
