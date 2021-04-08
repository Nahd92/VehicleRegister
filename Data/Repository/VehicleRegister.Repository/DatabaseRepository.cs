using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.Factory;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Repository
{
    public class DatabaseRepository : IVehicleRepository, IAutoMotiveRepairRepository, IServiceReservationsRepository
    {
        private readonly VehicleRegisterContext _ctx;
        public DatabaseRepository(VehicleRegisterContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CreateNewAutoMotive(IAutoMotiveRepair repair)
        {
            try
            {
                _ctx.Add(repair);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CreateVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Add(vehicle);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<bool> DeleteVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Remove(vehicle);
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
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
                return autoMotives;
            }
            catch (Exception ex)
            {

                throw ex;
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
                return vehicles;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IAutoMotiveRepair> GetAutoMotive(int id)
        {
            try
            {
                var autoMotive = await _ctx.AutoMotive.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

                if (autoMotive is null) return null;

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
                return repair;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IVehicle> GetVehicleById(int id)
        {
            try
            {
                var vehicle = await _ctx.Vehicles.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

                if (vehicle == null) return null;
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

                return vh;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        public Task<bool> UpdateAutMotive(IAutoMotiveRepair repair)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateVehicle(IVehicle vehicle)
        {
            try
            {
                _ctx.Update(vehicle);
                return await _ctx.SaveChangesAsync() > 0;                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
