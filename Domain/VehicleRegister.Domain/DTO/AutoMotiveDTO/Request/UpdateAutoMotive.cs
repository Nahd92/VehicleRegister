using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.DTO.AutoMotiveDTO.Request
{
   public class UpdateAutoMotive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
