using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Interfaces.Service.Interface
{
    public interface IAutoMotiveRepairService
    {
        Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives();
        Task<IAutoMotiveRepair> GetAutoMotiveById(int id);
        Task<bool> AddNewAutoMotiveToDatabase(AddAutoMotiveToListRequest request);
        Task<UpdatedAutoMotiveResponse> UpdateAutoMotive(UpdateAutoMotive request);
        Task<string> CreateOrganisationNumber();
        Task<bool> OrgansationNumberAlreadyExist(string number);
    }
}
