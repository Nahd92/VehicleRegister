using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleRegister.Domain.Interfaces.Model.Interface
{
    public interface IAutoMotiveRepair
    {
         int Id { get; set; }
         string Name { get; set; }
         int OrganisationNumber { get; set; }
         int PhoneNumber { get; set; }
         string Address { get; set; }
         string Website { get; set; }
         string City { get; set; }
         string Country { get; set; }
         string Description { get; set; }
    }
}
