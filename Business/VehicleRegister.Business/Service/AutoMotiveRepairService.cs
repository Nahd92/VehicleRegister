using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Business.Service
{
    public class AutoMotiveRepairService : IAutoMotiveRepairService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly ISpecialLoggerExtension _logger;
        public AutoMotiveRepairService(IRepositoryWrapper repo, ISpecialLoggerExtension logger)
        {
            _repo = repo;
            _logger = logger;
        }


        public async Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives()
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var autoMotives = await _repo.RepairRepo.GetAllAutoMotives();

                if (autoMotives.Count() != 0)
                {
                    _logger.LogInfo(GetType().Name, method, $"{autoMotives.Count()} fetched from database");
                    return autoMotives;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
                return null;
            }
            return null;
        }

        public async Task<IAutoMotiveRepair> GetAutoMotiveById(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var automotive = await _repo.RepairRepo.GetAutoMotive(id);
                if (automotive != null)
                {
                    _logger.LogInfo(GetType().Name, method, "AutoMotive as fetched");
                    return automotive;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
                return null;
            }
            return null;
        }

        public async Task<bool> AddNewAutoMotiveToDatabase(AddAutoMotiveToListRequest request)
        {
            var method = _logger.GetActualAsyncMethodName();

            try
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


                var IsCreated = await _repo.RepairRepo.CreateNewAutoMotive(newAutoMotive);

                if (IsCreated)
                {
                    _logger.LogInfo(GetType().Name, method, "Created was successful");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
            }
            return false;
        }

        public async Task<UpdatedAutoMotiveResponse> UpdateAutoMotive(UpdateAutoMotiveDto request)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var existingAutoMotive = await _repo.RepairRepo.GetAutoMotive(request.Id);

                if (existingAutoMotive != null)
                {
                    existingAutoMotive.Name = request.Name;
                    existingAutoMotive.Address = request.Address;
                    existingAutoMotive.City = request.City;
                    existingAutoMotive.Country = request.Country;
                    existingAutoMotive.Description = request.Description;
                    existingAutoMotive.PhoneNumber = request.PhoneNumber;
                    existingAutoMotive.Website = request.Website;

                   var IsUpdated = await _repo.RepairRepo.UpdateAutoMotive(existingAutoMotive);

                    if (IsUpdated)
                    {
                        _logger.LogInfo(GetType().Name, method, "AutoMotive was updated");
                        return new UpdatedAutoMotiveResponse(
                        existingAutoMotive.Id, existingAutoMotive.Name, existingAutoMotive.City,
                        existingAutoMotive.Country, existingAutoMotive.Description,
                        existingAutoMotive.PhoneNumber, existingAutoMotive.Website, existingAutoMotive.OrganisationNumber);
                  }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
            }
            return null;
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
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var automMotives = await _repo.RepairRepo.GetAllAutoMotives();

                if (automMotives != null)
                {
                    _logger.LogInfo(GetType().Name, method, $"{automMotives.Count()} was fetched from database");
                    if (automMotives.Any(x => x.OrganisationNumber == number))
                    {
                        _logger.LogInfo(GetType().Name, method, $"There was one automotive with organisationnumber: {number}");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(GetType().Name, ex, method);
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteAutoMotive(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var autoMotive = await _repo.RepairRepo.GetAutoMotive(id);

                if (autoMotive != null)
                {
                    _logger.LogInfo(GetType().Name, method, "Autotmotive was fetched");
                    var deleted = await _repo.RepairRepo.DeleteAutoMotive(autoMotive);
                    if (deleted )
                    {
                        _logger.LogInfo(GetType().Name, method, "AutoMotive was deleted");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(GetType().Name, ex, method);
            }
            return false;
        }
    }
}
