using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Extensions;
using VehicleRegister.Domain.Factory;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Business.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly ISpecialLoggerExtension _logger;
        public VehicleService(IRepositoryWrapper repo, ISpecialLoggerExtension logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<bool> CreateVehicle(CreateVehicleRequest vehicle)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var createdV = VehicleFactory.Create(0, vehicle.RegisterNumber, vehicle.Brand, vehicle.Model, vehicle.InTraffic, vehicle.IsDrivingBan, vehicle.IsServiceBooked, vehicle.Weight, vehicle.YearlyFee);
              
                if (createdV != null)               
                    _logger.LogInfo(this.GetType().Name, method, $"Vehicle created through factory, returns {createdV}");

                return await _repo.VehicleRepo.CreateVehicle(createdV);
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return false;
            }
        }

        public async Task<IEnumerable<IVehicle>> GetAllVehicles()
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicles = await _repo.VehicleRepo.GetAllVehicles();

                if (vehicles != null)
                    _logger.LogInfo(this.GetType().Name, method, $"{vehicles.Count()} Number of vehicles fetched");

                return vehicles;
            
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return null;
            }
           
        }

        public async Task<IVehicle> GetVehicleById(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicle =  await _repo.VehicleRepo.GetVehicleById(id);

                if (vehicle != null)
                    _logger.LogInfo(this.GetType().Name, method, $"{vehicle} was fetched from databas");

                return vehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return null;
            }
        }

        public async Task<bool> DeleteVehicle(int id)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicle = await _repo.VehicleRepo.GetVehicleById(id);

                if (vehicle != null)
                {
                    _logger.LogInfo(this.GetType().Name, method, $"{vehicle} was fetched from Database");

                    var deleted = await _repo.VehicleRepo.DeleteVehicle(vehicle);

                    if (deleted)
                        _logger.LogInfo(this.GetType().Name, method, $"{vehicle} was deleted from Database");

                    return true;
                }
              
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return false;
            }
        }

        public async Task<UpdateVehicleResponse> UpdateVehicle(UpdateVehicleRequest request)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicle = await _repo.VehicleRepo.GetVehicleById(request.Id);

                if (vehicle != null)
                    _logger.LogInfo(this.GetType().Name, method, $"{vehicle} was fetched from database");

                vehicle.IsDrivingBan = request.IsDrivingBan;
                vehicle.IsServiceBooked = request.IsServiceBooked;
                vehicle.ServiceDate = request.ServiceDate;

              var IsUpdated = await _repo.VehicleRepo.UpdateVehicle(vehicle);

                if (IsUpdated)
                    _logger.LogInfo(this.GetType().Name, method, $"{vehicle} is updated and returns new UpdateVehicleResponse");

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
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return null;
            }
            



         
        }

        public async Task<List<IVehicle>> GetVehicleWithKeyword(string keyword)
        {
            var method = _logger.GetActualAsyncMethodName();
            try
            {
                var vehicles = await _repo.VehicleRepo.GetAllVehicles();

                if (vehicles.Count() != 0)
                {
                    _logger.LogInfo(this.GetType().Name, method, $"fetching {vehicles.Count()} vehicles from database!");

                    var newList = new List<IVehicle>();
                    foreach (var item in vehicles.Where(y => y.RegisterNumber.ToUpper() == keyword.ToUpper()))
                    {
                        newList.Add(item);
                    }

                    foreach (var item in vehicles.Where(y => y.Brand.ToUpper() == keyword.ToUpper()))
                    {
                        newList.Add(item);
                    }

                    foreach (var item in vehicles.Where(x => x.Model.ToUpper() == keyword.ToUpper()))
                    {
                        newList.Add(item);
                    }
                    return newList;
                }
                return null;      
            }
            catch (Exception ex)
            {
                _logger.LogError(this.GetType().Name, ex, method);
                return null;
            }       
        }
    }
}
