using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VehicleRegister.Domain.DTO.UserDTO.Response;

namespace VehicleRegister.Domain.DTO.UserDTO.Request
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }
}
