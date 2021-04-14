using System;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Models
{
    public class VehicleServiceHistory : IVehicleServiceHistory
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
    
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        
        public int AutoMotiveRepairId { get; set; }
        public AutoMotiveRepair AutoMotiveRepair { get; set; }
    }
}
