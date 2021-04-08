using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.Interfaces.Auth.Interface;

namespace VehicleRegister.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

      

        public async Task <List<string>> GetUsersRole(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetClaimsAsync(user);
            var newList = new List<string>();

            foreach (var role in roles.Select(x => x.Value))
            {
                newList.Add(role);
            }
            return newList;
        }

        public async Task<bool> IsValidUserNameAndPassword(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
