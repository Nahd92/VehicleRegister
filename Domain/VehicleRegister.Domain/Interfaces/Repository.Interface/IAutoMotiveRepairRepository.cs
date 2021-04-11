using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IAutoMotiveRepairRepository
    {
        Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives();
        Task<IAutoMotiveRepair> GetAutoMotive(int id);
        Task<bool> CreateNewAutoMotive(IAutoMotiveRepair repair);
        Task<bool> UpdateAutoMotive(IAutoMotiveRepair repair);
        Task<bool> DeleteAutoMotive(IAutoMotiveRepair repair);
    }
}
