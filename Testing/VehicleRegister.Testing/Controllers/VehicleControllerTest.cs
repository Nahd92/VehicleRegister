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
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;


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

        private IEnumerable<IVehicle> car = new List<Vehicle>()
        {
            new Vehicle()
            {
                Id = 1,
                Brand = "Volvo",
                Model = "XC90",
                InTraffic = DateTime.Parse("2020-02-02"),
                IsDrivingBan = false,
                RegisterNumber = 123-431,
                Weight = 2220,
                IsServiceBooked = false,
                ServiceDate = DateTime.Parse("2021-03-03"),
                YearlyFee = 2000
            }
        };


        private CreateVehicleRequest carDTO = new CreateVehicleRequest()
        {
            Id = 1,
            Brand = "Volvo",
            Model = "XC90",
            InTraffic = DateTime.Parse("2020-02-02"),
            IsDrivingBan = false,
            RegisterNumber = 123 - 431,
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
            mockService.Setup(x => x.Vehicle.GetAllVehicles()).ReturnsAsync(car);

            //Act
            var response = await vehicleController.GetAllVehicles();

            //Assert
            var result = response.Should().BeOfType<JsonResult>().Subject;
            var vehicle = result.Value.Should().BeAssignableTo<IEnumerable<IVehicle>>().Subject;
            vehicle.Count().Should().Be(1);
        }



        [TestMethod]
        public async Task CreateVehicle_ShouldReturnOkWhenReturningTrue()
        {
            //Arrange
            mockService.Setup(x => x.Vehicle.CreateVehicle(It.IsAny<CreateVehicleRequest>())).ReturnsAsync(true);

            //Act
            var response = await vehicleController.CreateVehicle(carDTO);

            //Assert
            response.Should().BeOfType<OkResult>();
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



    }
}
