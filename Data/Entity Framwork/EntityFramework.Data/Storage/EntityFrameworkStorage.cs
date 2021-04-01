using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;

namespace EntityFramework.Data.Storage
{
    public class EntityFrameworkStorage : IVehicleRepository, IAutoMotiveRepairRepository, IServiceReservationsRepository
    {
        private readonly VehicleRegisterContext _ctx;
        public EntityFrameworkStorage(VehicleRegisterContext ctx)
        {
            _ctx = ctx;
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

    
        public async Task<IEnumerable<IVehicle>> GetAllVehicles()
        {
            try
            {
                var vehicles = new List<IVehicle>();

                foreach (var veh in await _ctx.Vehicles.ToListAsync())
                {
                    vehicles.Add(new Truck()
                    {
                        Id = veh.Id,
                        Brand = veh.Brand,
                        DrivingBan = veh.DrivingBan,
                        ServiceDate = veh.ServiceDate,
                        ServiceBooked = veh.ServiceBooked,
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
    }
}
