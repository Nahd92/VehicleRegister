using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.DTO.UserDTO.Response;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.VehicleAPI.Controllers;

namespace VehicleRegister.Testing.API.Controllers
{

    [TestClass]
    public class AuthControllerTest
    {

        private readonly Mock<IServiceWrapper> mockService;
        private AuthController authController;
        public AuthControllerTest()
        {
            mockService = new Mock<IServiceWrapper>();
            authController = new AuthController(mockService.Object);
        }



        private GetUserInformationDto userInformation = new GetUserInformationDto
        {
            Id = Guid.NewGuid().ToString(),
            Email = "mock@mock.com",
            UserName = "Mock92",
            PhoneNumber = "0703231232"
        };



        [TestMethod]
        public async Task GetUserGetUserInformation_ShouldReturnOk()
        {
            //Arrange
            mockService.Setup(x => x.authService.GetUserInformation(It.IsAny<string>())).ReturnsAsync(userInformation);
            //Act
            var response = await authController.GetUserInformation("Mock92");

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var authResult = result.Value.Should().BeOfType<GetUserInformationDto>().Subject;
            authResult.UserName.Should().Be("Mock92");
        }



        [TestMethod]
        public async Task GetUserGetUserInformation_ShouldReturnBadRequestWhenParameterIsNull()
        {
            //Arrange
            //Act
            var response = await authController.GetUserInformation(null);

            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Parameter is null");
        }

        [TestMethod]
        public async Task GetUserGetUserInformation_ShouldReturnBadRequestWhenNoUserExist()
        {
            //Arrange
            mockService.Setup(x => x.authService.GetUserInformation(null));
            //Act
            var response = await authController.GetUserInformation("Mock92");

            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Something happened when getting user information, try again");
        }


        [TestMethod]
        public async Task Token_ShouldReturnBadRequestIfParameterIsNull()
        {
            //Arrange
            //Act
            var response = await authController.Token(null);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Invalid request");
        }


        [TestMethod]
        public async Task Token_ShouldReturnUnauthorizedWhenUserNameDoesntExist()
        {
            //Arrange
            var request = new LoginRequest()
            {
                UserName = "Mock92",
                Password = "mock123"
            };
            mockService.Setup(x => x.authService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            //Act
            var response = await authController.Token(request);
            //Assert
            var result = response.Should().BeOfType<UnauthorizedResult>().Subject;
        }


        [TestMethod]
        public async Task RegisterUser_ShouldReturnBadRequestIfParameterIsNull()
        {
            //Arrange

            //Act
            var response = await authController.RegisterUser(null);
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Invalid request");
        }


        [TestMethod]
        public async Task RegisterUser_ShouldReturnBadRequestIfResultReturnsFalse()
        {
            //Arrange
            string userName = "Mock92";
            string password = "mock123";
            mockService.Setup(x => x.authService.RegisterUser(It.IsAny<RegisterUserRequest>())).ReturnsAsync(false);
            //Act
            var response = await authController.RegisterUser(new RegisterUserRequest { UserName = userName, Password = password });
            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            var authResult = result.Value.Should().Be("Registration of a new user was unsuccesfull");
        }

        [TestMethod]
        public async Task RegisterUser_ShouldReturnNoContentIfResultReturnsTrue()
        {
            //Arrange
            string userName = "Mock92";
            string password = "mock123";
            mockService.Setup(x => x.authService.RegisterUser(It.IsAny<RegisterUserRequest>())).ReturnsAsync(true);
            //Act
            var response = await authController.RegisterUser(new RegisterUserRequest { UserName = userName, Password = password });
            //Assert
            response.Should().BeOfType<NoContentResult>();
        }


        [TestMethod]
        public async Task Token_ShouldReturnOkAndWithAToken()
        {
            string token = CreateMockToken();
            var roles = new List<string> { "Admin" };


            mockService.Setup(x => x.authService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            mockService.Setup(x => x.authService.GetUsersRole(It.IsAny<string>())).ReturnsAsync(roles);
            mockService.Setup(x => x.authService.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>())).Returns(token);
            //Act


            var response = await authController.Token(new LoginRequest
            {
                UserName = "Mock92",
                Password = "Mock123"
            });


            //Assert
            mockService.Verify(x => x.authService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            mockService.Verify(x => x.authService.GetUsersRole(It.IsAny<string>()), Times.Once());
            mockService.Verify(x => x.authService.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()), Times.Once());

            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var authResult = result.Value.Should().BeOfType<LoginResponse>().Subject;
            authResult.Token.Should().NotBeNullOrEmpty();
        }

        private static string CreateMockToken()
        {
            //Arrange
            string Issuer = Guid.NewGuid().ToString();
            SecurityKey SecurityKey;
            SigningCredentials SigningCredentials;

            JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
            var s_rng = RandomNumberGenerator.Create();
            byte[] s_key = new byte[32];

            var claims = new List<Claim>();

            s_rng.GetBytes(s_key);
            SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var token = s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
            return token;
        }
    }
}
