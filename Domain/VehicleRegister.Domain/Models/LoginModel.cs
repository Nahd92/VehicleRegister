using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleRegister.Domain.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }

    }
}
