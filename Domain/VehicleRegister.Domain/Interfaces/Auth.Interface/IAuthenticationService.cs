using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Request;

namespace VehicleRegister.Domain.Interfaces.Auth.Interface
{
   public interface IAuthenticationService
    {
        Task<bool> IsValidUserNameAndPassword(string username, string password);
        Task<List<string>> GetUsersRole(string username);
        Task<bool> RegisterUser(RegisterUserRequest request);
    }
}
