using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.Domain.Interfaces.Model.Interface;

namespace VehicleRegister.Domain.Models
{
    public class AutoMotiveRepair : IAutoMotiveRepair
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganisationNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
