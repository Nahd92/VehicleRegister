using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.AutoMotiveDTO.Response
{
   public class UpdatedAutoMotiveResponse : AutoMotiveRepair
    {

        public UpdatedAutoMotiveResponse(int id, string name, string city, string country, 
                string description, int phoneNumber, string website, string organisationNumber)
        {
            Id = id;
            Name = name;
            City = city;
            Country = country;
            Description = description;
            PhoneNumber = phoneNumber;
            Website = website;
            OrganisationNumber = organisationNumber;
        }
    }
}
