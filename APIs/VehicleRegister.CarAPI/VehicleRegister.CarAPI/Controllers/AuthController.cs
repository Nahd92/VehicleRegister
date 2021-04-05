using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.Interfaces.Auth.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.RouteAPI;

namespace VehicleRegister.VehicleAPI.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IServiceWrapper _service;

        public AuthController(IServiceWrapper service)
        {
            _service = service;
        }


        [HttpPost]
        [Route(RoutesAPI.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            if (request == null) return BadRequest("Invalid request");

           var validation =  await _service.authService.IsValidUserNameAndPassword(request.UserName, request.Password);

            if (validation)
            {

                var usersRole = await _service.authService.GetUsersRole(request.UserName);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.UserName),
                    new Claim(ClaimTypes.Role, usersRole)
                };

               var accessToken = _service.authService.GenerateAccessToken(claims);
    
               return Ok(new { Token = accessToken });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
