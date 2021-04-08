using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces.Auth.Interface
{
   public interface IAuthenticationService
    {
        Task<bool> IsValidUserNameAndPassword(string username, string password);
        Task<List<string>> GetUsersRole(string username);
    }
}
