using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.CarAPI.Controllers;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.DTO.VehicleDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Testing.Controllers
{
    [TestClass]
   public class VehicleControllerTest
    {
        private readonly Mock<IServiceWrapper> mockService;
        private VehicleController vehicleController;
        public VehicleControllerTest()
        {
            mockService = new Mock<IServiceWrapper>();
            vehicleController = new VehicleController(mockService.Object);
        }

        private IEnumerable<IVehicle> vehicles = null;

        private IEnumerable<IVehicle> cars = new List<Vehicle>()
        {
            new LightVehicle()
            {
                Id = 1,
                Brand = "Volvo",
                Model = "XC90",
                InTraffic = DateTime.Parse("2020-02-02"),
                IsDrivingBan = false,
                RegisterNumber = "ABC123",
                Weight = 2220,
                IsServiceBooked = false,
                ServiceDate = DateTime.Parse("2021-03-03"),
                YearlyFee = 2000
            }
        };
        private IVehicle car = new LightVehicle()
        {
            Id = 1,
            Brand = "Volvo",
            Model = "XC90",
            InTraffic = DateTime.Parse("2020-02-02"),
            IsDrivingBan = false,
            RegisterNumber = "ABC123",
            Weight = 2220,
            IsServiceBooked = false,
            ServiceDate = DateTime.Parse("2021-03-03"),
            YearlyFee = 2000
        };

        private CreateVehicleRequest carDTO = new CreateVehicleRequest()
        {
            Brand = "Volvo",
            Model = "XC90",
            InTraffic = DateTime.Parse("2020-02-02"),
            IsDrivingBan = false,
            RegisterNumber = "ABC123",
            Weight = 2220,
            IsServiceBooked = false,
            ServiceDate = DateTime.Parse("2021-03-03"),
            YearlyFee = 2000
        };




        [TestMethod]
        public async Task GetAllVehicles_ShouldReturnNotFoundWhenListIsEmpty()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.GetAllVehicles()).ReturnsAsync(vehicles);

            //Act
            var response = await vehicleController.GetAllVehicles();

            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetAllVehicles_ShouldReturnOkWhenListIsNotEmpty()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.GetAllVehicles()).ReturnsAsync(cars);

            //Act
            var response = await vehicleController.GetAllVehicles();

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<List<Vehicle>>().Subject;
            vehicle.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task GetVehicleByRegNumber_ShouldReturNoContent()
        {
            //Arrange
            var regNumber = "ABC123";
            //mockService.Setup(x => x.Vehicle.GetVehicleWithKeyword(It.IsAny<string>())).ReturnsAsync(cars);

            //Act
            var response = await vehicleController.GetVehicleWithKeyword(regNumber);

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<Vehicle>().Subject;
            vehicle.RegisterNumber.Should().Be("ABC123");
        }

        [TestMethod]
        public async Task GetVehicleByRegNumber_ShouldReturNotFoundWhenVehicleIsNull()
        {
            //Arrange
            var regNumber = "ABC123";
            mockService.Setup(x => x.Vehicle.GetVehicleWithKeyword(It.IsAny<string>()));

            //Act
            var response = await vehicleController.GetVehicleWithKeyword(regNumber);

            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task CreateVehicle_ShouldReturnOkWhenReturningTrue()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.CreateVehicle(It.IsAny<CreateVehicleRequest>())).ReturnsAsync(true);

            //Act
            var response = await vehicleController.CreateVehicle(carDTO);

            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task CreateVehicle_ShouldReturnBadRequestWhenReturningFalse()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.CreateVehicle(It.IsAny<CreateVehicleRequest>())).ReturnsAsync(false);

            //Act
            var response = await vehicleController.CreateVehicle(carDTO);

            //Assert
           var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Something happened while trying to create a new Vehicle, try again!");
        }

        [TestMethod]
        public async Task GetVehicleById_ShouldReturnNotFoundIfVehicleNotExist()
        {
            //Arrange
            IVehicle nullValue = null;
            int randomId = 1;
            mockService.Setup(x => x.Vehicle.GetVehicleById(It.IsAny<int>())).ReturnsAsync(nullValue);

            //Act
            var response = await vehicleController.GetVehicle(randomId);

            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetVehicleById_ShouldReturnOkAndValueWhenVehicleExist()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);

            //Act
            var response = await vehicleController.GetVehicle(car.Id);

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var re = result.Value.Should().BeOfType<Vehicle>().Subject;
            re.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task DeleteVehicle_ShouldReturnNoContentWhenSuccessfully()
        {
            //Arrange
            int randomId = 1;
            mockService.Setup(x => x.Vehicle.DeleteVehicle(It.IsAny<int>())).ReturnsAsync(true);

            //Act
            var response = await vehicleController.DeleteVehicle(randomId);

            //Assert
            var result = response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task DeleteVehicle_ShouldReturnBadRequestAndASpecificMessageToUser()
        {
            //Arrange
            int randomId = 1;
            mockService.Setup(x => x.Vehicle.DeleteVehicle(It.IsAny<int>())).ReturnsAsync(false);

            //Act
            var response = await vehicleController.DeleteVehicle(randomId);

            //Assert
            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Something happend when trying to delete vehicle! Try again");
        }


        [TestMethod]
        public async Task UpdateVehicle_ShouldReturnOkResultAndCorrectType()
        {
            //Arrange
           var request = new UpdateVehicleResponse() { Id = 1, Brand = "Ferrari", Model = "Diablo", ServiceDate = DateTime.Now, InTraffic = DateTime.Now, IsDrivingBan = false, IsServiceBooked = false, YearlyFee = 2000, RegisterNumber = "ABC123", Weight = 2200 };
            mockService.Setup(x => x.Vehicle.UpdateVehicle(It.IsAny<UpdateVehicleRequest>()))
                                            .ReturnsAsync(request);

            //Act
            var response = await vehicleController.UpdateVehicle(new UpdateVehicleRequest() { Id = 1, IsDrivingBan = false, IsServiceBooked = false, ServiceDate = DateTime.Now });

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<UpdateVehicleResponse>().Subject;
            vehicle.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task UpdateVehicle_ShouldReturnNotFoundAndMessage()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.UpdateVehicle(It.IsAny<UpdateVehicleRequest>()));

            //Act
            var response = await vehicleController.UpdateVehicle(new UpdateVehicleRequest() { Id = 1, IsDrivingBan = false, IsServiceBooked = false, ServiceDate = DateTime.Now });

            //Assert
            var result = response.Should().BeOfType<NotFoundObjectResult>().Subject;
            result.Value.Should().Be("Could not find any Vehicles with inputed Id");
        }

    }
}
