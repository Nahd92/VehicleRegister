using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace VehicleRegister.Domain.DTO.UserDTO.Response
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
