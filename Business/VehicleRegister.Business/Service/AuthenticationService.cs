using EntityFramework.Data;
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
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ILoggerManager _logger;

        public AuthenticationService(UserManager<IdentityUser> userManager, ILoggerManager logger)
        {
            _userManager = userManager;
            _logger = logger;
        }



        public async Task<bool> RegisterUser(RegisterUserRequest request)
        {

            var UserNameExist = await UserNameAlreadyExist(request);

            if (UserNameExist) return false;

            var PasswordIsEqual = PasswordValidation(request);

            if (PasswordIsEqual)
            {
                try
                {
                    var newIdentity = new IdentityUser()
                    {
                        UserName = request.UserName
                    };

                    await _userManager.CreateAsync(newIdentity, request.Password);

                    var addClaim = new Claim("Role", "User");

                    await _userManager.AddClaimAsync(newIdentity, addClaim);
                    return true;
                }
                catch (Exception ex)
                {
                
                    throw ex;
                }
            }
            return false;
        }
        private bool PasswordValidation(RegisterUserRequest request)
        {
            if (request.Password == request.ConfirmPassword)
                return true;

            return false;
        }

        private async Task<bool> UserNameAlreadyExist(RegisterUserRequest request)
        {
            var id = new IdentityUser
            {
                UserName = request.UserName
            };

            var user = await _userManager.GetUserNameAsync(id);

            if (user is null) return false;

            return true;
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
