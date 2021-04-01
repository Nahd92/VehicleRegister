using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IAutoMotiveRepairService
    {
        Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives();
    }
}
