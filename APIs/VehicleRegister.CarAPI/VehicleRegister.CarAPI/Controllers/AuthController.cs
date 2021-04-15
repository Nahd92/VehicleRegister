using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.AppSettingsModels;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.DTO.UserDTO.Response;
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



        [HttpGet]
        [Authorize]
        [Route(RoutesAPI.Identity.GetUserInformation)]
        public async Task<IActionResult> GetUserInformation(string username)
        {
            if (username is null) return BadRequest("Parameter is null");

            var result = await _service.authService.GetUserInformation(username);

            if (result != null)
                return Ok(result);

            return BadRequest("Something happened when getting user information, try again");
        }


        [HttpPost]
        [Route(RoutesAPI.Identity.Register)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            if (request is null) return BadRequest("Invalid request");

           var result = await _service.authService.RegisterUser(request);

            if (result)
                return NoContent();

            return BadRequest("Registration of a new user was unsuccesfull");
        }

        

        [HttpPost]
        [Route(RoutesAPI.Identity.Token)]
        public async Task<IActionResult> Token([FromBody]LoginRequest request)
        {
            if (request is null) return BadRequest("Invalid request");

            var validation = await _service.authService.IsValidUserNameAndPassword(request.UserName, request.Password);

            if (validation)
            {

                var usersRole = await _service.authService.GetUsersRole(request.UserName);

                var roles = new List<string>();

             var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.UserName),                  
                };
                foreach (var claim in usersRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, claim));
                    roles.Add(claim);
                };

                var accessToken = _service.authService.GenerateAccessToken(claims);


                var loginModel = new LoginResponse
                {
                    Token = accessToken,
                    Roles = roles,
                    IsLoggedIn = true
                };
            
                return Ok(loginModel);
            }
            else
            {
                return Unauthorized();
            }
        }
       

    }
}
