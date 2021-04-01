using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Models
{
    public class AutoMotiveRepair
    {
        [Key]
        public int Id { get; set; }
    }
}
