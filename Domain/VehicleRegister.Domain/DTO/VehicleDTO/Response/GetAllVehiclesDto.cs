using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleRegister.Domain.DTO.VehicleDTO.Response
{
    public class GetAllVehiclesDto
    {
        public int Id { get; set; }
        public string RegisterNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Weight { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime InTraffic { get; set; }
        public bool IsDrivingBan { get; set; }
        public bool IsServiceBooked { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ServiceDate { get; set; }
        public int YearlyFee { get; set; }

 
        public string SearchKeyword { get; set; }
    }
}
