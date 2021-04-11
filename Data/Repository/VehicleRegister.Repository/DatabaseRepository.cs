using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.Factory;
using VehicleRegister.Domain.Interfaces.Logger.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Repository
{
    public class DatabaseRepository : IVehicleRepository, IAutoMotiveRepairRepository, IServiceReservationsRepository
    {
        private readonly VehicleRegisterContext _ctx;
        private ILoggerManager _logger;
        public DatabaseRepository(VehicleRegisterContext ctx, ILoggerManager logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
         

        private void ErrorLog(Exception ex, string name) => _logger.LogError($"{this.GetType().Name} with Method {name} resulted in: {ex.Message}.");
        private static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
        private void LogSuccessInfo(string name) => _logger.LogInfo($"{this.GetType().Name} with Method {name} was successfull");
        private void LogGettingInfo(string name) => _logger.LogInfo($"{this.GetType().Name} with Method {name} was successfull fetched from database");




        public async Task<bool> CreateNewAutoMotive(IAutoMotiveRepair repair)
        {

            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Add(repair);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> CreateReservations(IServiceReservations reservation)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Add(reservation);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> CreateVehicle(IVehicle vehicle)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Add(vehicle);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
            
        }

        public async Task<bool> DeleteAllReservations(List<IServiceReservations> reservations)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.RemoveRange(reservations);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;   
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> DeleteReservation(IServiceReservations reservations)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Remove(reservations);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> DeleteVehicle(IVehicle vehicle)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Remove(vehicle);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives()
        {
            var methodName = GetActualAsyncMethodName();
            try
            {

                var autoMotives = new List<IAutoMotiveRepair>();
                foreach (var auto in await _ctx.AutoMotive.ToListAsync())
                {
                    
                    autoMotives.Add(new AutoMotiveRepair()
                    {
                        Id = auto.Id,
                        Name = auto.Name,
                        Address = auto.Address,
                        City = auto.City,
                        Country = auto.Country,
                        Description = auto.Description,
                        OrganisationNumber = auto.OrganisationNumber,
                        PhoneNumber = auto.PhoneNumber,
                        Website = auto.Website
                    });
                }
                LogGettingInfo(methodName);
                return autoMotives;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }
        }

        public async Task<IEnumerable<IServiceReservations>> GetAllReservations()
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var serviceReservations = new List<IServiceReservations>();

                foreach (var reservation in await _ctx.ServiceReservations.ToListAsync())
                {                   
                    serviceReservations.Add(reservation);
                }
                LogGettingInfo(methodName);
                return serviceReservations;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }
        }

        public async Task<IEnumerable<IVehicle>> GetAllVehicles()
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var vehicles = new List<IVehicle>();

                foreach (var veh in await _ctx.Vehicles.ToListAsync())
                {
                   
                    var vehicle = VehicleFactory.Create(
                                                    veh.Id,
                                                    veh.RegisterNumber,
                                                    veh.Brand,
                                                    veh.Model,
                                                    veh.InTraffic,
                                                    veh.IsDrivingBan,
                                                    veh.IsServiceBooked,
                                                    veh.Weight,
                                                    veh.YearlyFee);
                    vehicles.Add(vehicle);
                }
                LogGettingInfo(methodName);
                return vehicles;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }

        }

        public async Task<IAutoMotiveRepair> GetAutoMotive(int id)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var autoMotive = await _ctx.AutoMotive.Where(x => x.Id == id).AsNoTracking().SingleAsync(); 

                IAutoMotiveRepair repair = new AutoMotiveRepair
                {
                    Id = autoMotive.Id,
                    Address = autoMotive.Address,
                    City = autoMotive.City,
                    Country = autoMotive.Country,
                    Description = autoMotive.Description,
                    Name = autoMotive.Name,
                    OrganisationNumber = autoMotive.OrganisationNumber,
                    PhoneNumber = autoMotive.PhoneNumber,
                    Website = autoMotive.Website
                };

                LogGettingInfo(methodName);
                return repair;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }
        }

        public async Task<IServiceReservations> GetReservation(int id)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var reservation = await _ctx.ServiceReservations.Where(x => x.Id == id).SingleAsync();
                LogGettingInfo(methodName);
                return reservation;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }        
        }

        public async Task<IVehicle> GetVehicleById(int id)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var vehicle = await _ctx.Vehicles.Where(x => x.Id == id).AsNoTracking().SingleAsync();


                var vh = VehicleFactory.Create(vehicle.Id,
                                                 vehicle.RegisterNumber,
                                                 vehicle.Brand,
                                                 vehicle.Model,
                                                 vehicle.InTraffic,
                                                 vehicle.IsDrivingBan,
                                                 vehicle.IsServiceBooked,
                                                 vehicle.Weight,
                                                 vehicle.YearlyFee);

                LogGettingInfo(methodName);
                return vh;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return null;
            }    
        }

        public async Task<bool> UpdateAutoMotive(IAutoMotiveRepair repair)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Update(repair);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> UpdateReservations(IServiceReservations request)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Update(request);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> UpdateVehicle(IVehicle vehicle)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Update(vehicle);
                LogSuccessInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;                
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> DeleteAutoMotive(IAutoMotiveRepair repair)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Remove(repair);
                LogGettingInfo(methodName);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                throw;
            }
        }
    }
}
