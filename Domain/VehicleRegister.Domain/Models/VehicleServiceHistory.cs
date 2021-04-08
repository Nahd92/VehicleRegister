using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Models
{
    public class VehicleServiceHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    
        public int VehicleMiles { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        
        public int AutoMotiveRepairId { get; set; }
        public AutoMotiveRepair AutoMotiveRepair { get; set; }
    }
}
