using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

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

        public async Task<IAutoMotiveRepair> GetAutoMotiveById(int id) =>
                await _repo.RepairRepo.GetAutoMotive(id);


        public async Task<bool> AddNewAutoMotiveToDatabase(AddAutoMotiveToListRequest request)
        {
            var newAutoMotive = new AutoMotiveRepair()
            {
                Id = request.Id,
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Description = request.Description,
                PhoneNumber = request.PhoneNumber,
                Website = request.Website,
                OrganisationNumber = await CreateOrganisationNumber()
            };


           return await _repo.RepairRepo.CreateNewAutoMotive(newAutoMotive);
        }

        public async Task<UpdatedAutoMotiveResponse> UpdateAutoMotive(UpdateAutoMotive request)
        {
            var existingAutoMotive = await _repo.RepairRepo.GetAutoMotive(request.Id);

            if (existingAutoMotive is null) return null;

            existingAutoMotive.Name = request.Name;
            existingAutoMotive.Address = request.Address;
            existingAutoMotive.City = request.City;
            existingAutoMotive.Country = request.Country;
            existingAutoMotive.Description = request.Description;
            existingAutoMotive.PhoneNumber = request.PhoneNumber;
            existingAutoMotive.Website = request.Website;

            await _repo.RepairRepo.UpdateAutMotive(existingAutoMotive);

            return new UpdatedAutoMotiveResponse(
                existingAutoMotive.Id, existingAutoMotive.Name, existingAutoMotive.City, 
                existingAutoMotive.Country, existingAutoMotive.Description, 
                existingAutoMotive.PhoneNumber, existingAutoMotive.Website, existingAutoMotive.OrganisationNumber);
        }

        public async Task<string> CreateOrganisationNumber()
        {
            var result = new int[10];
            var random = new Random();
            string stringResult = string.Empty;
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = random.Next(0, 9);
                }
                stringResult = string.Join("", result);
            } while (await OrgansationNumberAlreadyExist(stringResult));
            return stringResult;
        }

        public async Task<bool> OrgansationNumberAlreadyExist(string number)
        {
            var automMotives = await _repo.RepairRepo.GetAllAutoMotives();

            if (automMotives.Any(x => x.OrganisationNumber == number))
                return true;

            return false;
        }
    }
}
