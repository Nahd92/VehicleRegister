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
        Task<string> GetUsersRole(string username);
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
