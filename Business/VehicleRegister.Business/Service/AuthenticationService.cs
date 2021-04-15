using Microsoft.AspNetCore.Identity;
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
using VehicleRegister.Domain.DTO.UserDTO.Response;
using VehicleRegister.Domain.Extensions;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Business.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISpecialLoggerExtension _logger;


        public AuthenticationService(UserManager<IdentityUser> userManager, ISpecialLoggerExtension logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<bool> RegisterUser(RegisterUserRequest request)
        {
            var methodname = _logger.GetActualAsyncMethodName();
            var UserNameExist = await UserNameAlreadyExist(request);

            if (UserNameExist)
            {
                _logger.LogInfo(this.GetType().Name, methodname, "User already exist!");
                return false;
            }
                

            var PasswordIsEqual = PasswordValidation(request);

            if (PasswordIsEqual)
            {
                try
                {
                    var newIdentity = new IdentityUser()
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber
                    };

                    var createdUser = await _userManager.CreateAsync(newIdentity, request.Password);

                    if (createdUser.Succeeded)
                    {
                        var addClaim = new Claim("Role", "User");
                      var createdAddingClaim = await _userManager.AddClaimAsync(newIdentity, addClaim);
                        if (createdAddingClaim.Succeeded)
                        {
                            _logger.LogInfo(this.GetType().Name, methodname, "Returns True!");
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.ErrorLog(GetType().Name, ex, methodname);
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
            var user = await _userManager.FindByNameAsync(request.UserName);

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

        public async Task<GetUserInformationDto> GetUserInformation(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
                return new GetUserInformationDto 
                {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
                }; 
                       
            return null;           
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.SecretKey));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
              issuer: AppSettings.HostName,
              audience: AppSettings.HostName,
              claims: claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: signInCredentials
              );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
