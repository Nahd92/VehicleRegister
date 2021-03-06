using System;
using System.ComponentModel.DataAnnotations;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Domain.DTO.VehicleDTO.Request
{
    public class CreateVehicleRequest
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]{3}[0-9]{3}$", 
            ErrorMessage = "RegisterNumber has start with 3 letters and 3 numbers, without a space in the middle")]
        public string RegisterNumber { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Weight { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime InTraffic { get; set; }

        public bool IsDrivingBan { get; set; }
        public bool IsServiceBooked { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", ConvertEmptyStringToNull = true)]
        public Nullable<DateTime> ServiceDate { get; set; }
        public int YearlyFee { get; set; }

        public CreateVehicleRequest()
        {

        }

        public CreateVehicleRequest(string registerNumber, string model, string brand, int weight, DateTime inTraffic, bool isDrivingBan, bool isServiceBooked, DateTime serviceDate)
        {
            RegisterNumber = registerNumber;
            Model = model;
            Brand = brand;
            Weight = weight;
            InTraffic = inTraffic;
            IsDrivingBan = isDrivingBan;
            IsServiceBooked = isServiceBooked;
            ServiceDate = serviceDate;
        }

        public CreateVehicleRequest(int yearlyFee, DateTime serviceDate, bool isServiceBooked, bool isDrivingBan, DateTime inTraffic, int weight, string brand, string model, int id, string registerNumber)
        {
            YearlyFee = yearlyFee;
            ServiceDate = serviceDate;
            IsServiceBooked = isServiceBooked;
            IsDrivingBan = isDrivingBan;
            InTraffic = inTraffic;
            Weight = weight;
            Brand = brand;
            Model = model;
            Id = id;
            RegisterNumber = registerNumber;
        }
    }
}
