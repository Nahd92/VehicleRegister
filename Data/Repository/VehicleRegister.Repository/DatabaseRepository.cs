using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Repository
{
    public class DatabaseRepository : IVehicleRepository, IAutoMotiveRepairRepository, IServiceReservationsRepository
    {
        private readonly VehicleRegisterContext _ctx;
        public DatabaseRepository(VehicleRegisterContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CreateVehicle(IVehicle vehicle)
        {
            _ctx.Add(vehicle);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteVehicle(IVehicle vehicle)
        {
            _ctx.Remove(vehicle);
           return await _ctx.SaveChangesAsync() > 0;
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
                    vehicles.Add(new Vehicle()
                    {
                        Id = veh.Id,
                        Brand = veh.Brand,
                        IsDrivingBan = veh.IsDrivingBan,
                        ServiceDate = veh.ServiceDate,
                        IsServiceBooked = veh.IsServiceBooked,
                        InTraffic = veh.InTraffic,
                        Model = veh.Model,
                        RegisterNumber = veh.RegisterNumber,
                        Weight = veh.RegisterNumber,
                        YearlyFee = veh.YearlyFee
                    });
                }
                return vehicles;
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
                var vehicle = await _ctx.Vehicles.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (vehicle == null) return null;
                    
                IVehicle ve = new Vehicle()
                {
                    Id = vehicle.Id,
                    Brand = vehicle.Brand,
                    IsDrivingBan = vehicle.IsDrivingBan,
                    ServiceDate = vehicle.ServiceDate,
                    IsServiceBooked = vehicle.IsServiceBooked,
                    InTraffic = vehicle.InTraffic,
                    Model = vehicle.Model,
                    RegisterNumber = vehicle.RegisterNumber,
                    Weight = vehicle.RegisterNumber,
                    YearlyFee = vehicle.YearlyFee
                };

                return ve;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        public Task<bool> UpdateVehicle(IVehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
