﻿using FluentAssertions;
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

        private IEnumerable<IVehicle> cars = new List<Vehicle>()
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
        private IVehicle car = new Vehicle()
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

        private CreateVehicleRequest carDTO = new CreateVehicleRequest()
        {
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
            mockService.Setup(x => x.Vehicle.GetAllVehicles()).ReturnsAsync(cars);

            //Act
            var response = await vehicleController.GetAllVehicles();

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var vehicle = result.Value.Should().BeOfType<List<Vehicle>>().Subject;
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


    }
}
