using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.Extensions;
using VehicleRegister.Domain.Factory;
using VehicleRegister.Domain.Interfaces.Logger.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Repository
{
    public class DatabaseRepository : SpecialLoggerExtensions, 
                        IVehicleRepository, IAutoMotiveRepairRepository, IServiceReservationsRepository,
                        IVehicleServiceHistoryRepository
    {
        private readonly VehicleRegisterContext _ctx;

        public DatabaseRepository(VehicleRegisterContext ctx, ILoggerManager logger) : base(logger)
        {
            _ctx = ctx;
        }

        public async Task<bool> CreateNewAutoMotive(IAutoMotiveRepair repair)
        {

            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.Add(repair);
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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

                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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


        public async Task<IEnumerable<IVehicleServiceHistory>> VehicleHistory()
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                var history = new List<IVehicleServiceHistory>();
                foreach (var item in await _ctx.VehicleServiceHistory.ToListAsync())
                {
                    history.Add(item);
                }
                LogGettingInfo(methodName);
                return history;
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
                 LogSuccessInfo(methodName);

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
                                                 vehicle.ServiceDate,
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
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
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

        public async Task<bool> AddOldServiceToHistory(IVehicleServiceHistory oldService)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {               
                _ctx.Add(oldService);
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }
        public async Task<bool> AddOldServicesToHistory(List<IVehicleServiceHistory> oldServices)
        {
            var methodName = GetActualAsyncMethodName();
            try
            {
                _ctx.AddRange(oldServices);
                if (await _ctx.SaveChangesAsync() > 0)
                    LogSuccessInfo(methodName);

                return true;
            }
            catch (Exception ex)
            {
                ErrorLog(ex, methodName);
                return false;
            }
        }

    }
}
