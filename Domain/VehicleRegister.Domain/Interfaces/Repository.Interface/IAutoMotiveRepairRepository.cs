using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IAutoMotiveRepairRepository
    {
        Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives();
    }
}
