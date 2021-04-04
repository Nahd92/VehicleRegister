using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Testing.Services
{
    [TestClass]
    public class VehicleServiceTest
    {
        private readonly Mock<IRepositoryWrapper> mockService;
        private VehicleService vehicleService;
        public VehicleServiceTest()
        {
            mockService = new Mock<IRepositoryWrapper>();
            vehicleService = new VehicleService(mockService.Object);
        }

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
                RegisterNumber = 123-431,
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
                RegisterNumber = 123-431,
                Weight = 2220,
                IsServiceBooked = false,
                ServiceDate = DateTime.Parse("2021-03-03"),
                YearlyFee = 2000        
        };




        [TestMethod]
        public async Task TestGetAllVehicles_ShouldContainOneVehicleInList()
        {
            //arrange
            mockService.Setup(x => x.VehicleRepo.GetAllVehicles()).ReturnsAsync(cars);

            //Act
            var response = await vehicleService.GetAllVehicles();

            //Assert
            Assert.AreEqual(1, response.Count());
        } 

        [TestMethod]
        public async Task TestCreateVehicle_ShouldReturnCreatedResponse()
        {
            //Arrange
            mockService.Setup(x => x.VehicleRepo.CreateVehicle(It.IsAny<IVehicle>())).ReturnsAsync(true);
            //Act
            var response = await vehicleService.CreateVehicle(carDTO);
            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task TestGetVehicle_ShouldReturnCorrectId()
        {
            //Arrange
            mockService.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);
            //Act
            var response = await vehicleService.GetVehicleById(car.Id);
            //Assert
           response.Id.Should().Be(1);
        }
    }
}
