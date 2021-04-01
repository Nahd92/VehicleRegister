using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;

namespace VehicleRegister.Business.Service
{
    public class AutoMotiveRepairService : IAutoMotiveRepairService
    {
        private readonly IRepositoryWrapper _repo;
        public AutoMotiveRepairService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives() => await _repo.RepairRepo.GetAllAutoMotives();
    }
}
