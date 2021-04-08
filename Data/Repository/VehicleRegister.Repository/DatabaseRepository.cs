﻿using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CreateNewAutoMotive(IAutoMotiveRepair repair)
        {
            try
            {
                _ctx.Add(repair);
                _logger.LogInfo("AutoMotive was created");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AutoMotive was not created and gave error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateReservations(IServiceReservations reservation)
        {
            try
            {
                _ctx.Add(reservation);
                _logger.LogInfo("Reservation was created");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservation was not created and gave error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Add(vehicle);
                _logger.LogInfo("Vehicle was created");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
               _logger.LogError($"Reservation was not created and gave error: {ex.Message}");
                return false;
            }
            
        }

        public async Task<bool> DeleteAllReservations(List<IServiceReservations> reservations)
        {
            try
            {
                _ctx.RemoveRange(reservations);
                _logger.LogInfo("Every reservation was deleted from today and 30 days back!");
                return await _ctx.SaveChangesAsync() > 0;   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete all Reservations was not successfull and gave error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteReservation(IServiceReservations reservations)
        {
            try
            {
                _ctx.Remove(reservations);
                _logger.LogInfo("DEletion of one reservation was successful");
               return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete reservation was not Successfull and gave error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Remove(vehicle);
                _logger.LogInfo("DEletion of one Vehicle was successful");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete Vehicle was not Successfull and gave error: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<IAutoMotiveRepair>> GetAllAutoMotives()
        {
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
                _logger.LogInfo($"Adding {_ctx.AutoMotive.ToList().Count()} to new list and returns back to client! ");
                return autoMotives;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }
        }

        public async Task<IEnumerable<IServiceReservations>> GetAllReservations()
        {
            try
            {
                var serviceReservations = new List<IServiceReservations>();

                foreach (var reservation in await _ctx.ServiceReservations.ToListAsync())
                {                   
                    serviceReservations.Add(reservation);
                }
                _logger.LogInfo($"Adding {_ctx.ServiceReservations.ToList().Count()} to new list and returns back to client! ");
                return serviceReservations;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }
        }

        public async Task<IEnumerable<IVehicle>> GetAllVehicles()
        {
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
                                                    veh.ServiceDate,
                                                    veh.Weight,
                                                    veh.YearlyFee);
                    vehicles.Add(vehicle);
                }
                _logger.LogInfo($"Adding {_ctx.Vehicles.ToList().Count()} to new list and returns back to client! ");
                return vehicles;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }

        }

        public async Task<IAutoMotiveRepair> GetAutoMotive(int id)
        {
            try
            {
                var autoMotive = await _ctx.AutoMotive.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

                if (autoMotive is null)
                {
                    _logger.LogError($"No AutoMotive was found in Database with id: {id}, returns back null value");
                    return null;
                }

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

                _logger.LogInfo($"Adding {repair.Id} to new list and returns back to client! ");
                return repair;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }
        }

        public async Task<IServiceReservations> GetReservation(int id)
        {
            try
            {
                var reservation = await _ctx.ServiceReservations.Where(x => x.Id == id).SingleOrDefaultAsync();

                if (reservation == null) 
                {
                    _logger.LogError($"No AutoMotive was found in Database with id: {id}, returns back null value");
                    return null;
                }

                return reservation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }        
        }

        public async Task<IVehicle> GetVehicleById(int id)
        {
            try
            {
                var vehicle = await _ctx.Vehicles.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

                if (vehicle == null)
                {
                    _logger.LogError($"No vehicle was found in Database with id: {id}, returns back null value");
                    return null;
                }


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

                _logger.LogInfo($"Adding {vh.Id} to new list and returns back to client! ");
                return vh;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message} and returns null");
                return null;
            }    
        }

        public async Task<bool> UpdateAutMotive(IAutoMotiveRepair repair)
        {
            try
            {
                _ctx.Update(repair);
                _logger.LogError("Updating AutoMotive was success");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateReservations(IServiceReservations request)
        {
            try
            {
                _ctx.Update(request);
                _logger.LogError("Updating Reservation was success");
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Update(vehicle);
                _logger.LogError("Updating Vehicle was success");
                return await _ctx.SaveChangesAsync() > 0;                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message: {ex.Message}");
                return false;
            }
        }
    }
}
