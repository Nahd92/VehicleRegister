using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.RouteAPI;
using VehicleRegister.VehicleAPI.Helper.AppsettingsHelper;

namespace VehicleRegister.VehicleAPI.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IServiceWrapper _service;
        private AppSettings _appSettings;
        public AuthController(IServiceWrapper service, AppSettings appSettings)
        {
            _service = service;
            _appSettings = appSettings;
        }

        [HttpPost]
        [Route(RoutesAPI.Identity.Token)]
        public async Task<IActionResult> Token([FromBody]LoginRequest request)
        {
            if (request == null) return BadRequest("Invalid request");

            var validation = await _service.authService.IsValidUserNameAndPassword(request.UserName, request.Password);

            if (validation)
            {

                var usersRole = await _service.authService.GetUsersRole(request.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.UserName),                  
                };
                foreach (var claim in usersRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, claim));
                };



                var accessToken = GenerateAccessToken(claims);



                HttpContext.Response.Cookies.Append("access_token", accessToken, new CookieOptions { HttpOnly = true, Secure = true });
                return Ok(new { Token = accessToken });
            }
            else
            {
                return Unauthorized();
            }
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
              issuer: _appSettings.HostName,
              audience: _appSettings.HostName,
              claims: claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: signInCredentials
              );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

    }
}
