using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.DTO.VehicleDTO.Request;
using VehicleRegister.Domain.Factory;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Testing.Services
{
    [TestClass]
    public class VehicleServiceTest
    {
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private VehicleService vehicleService;
        public VehicleServiceTest()
        {
            mockRepository = new Mock<IRepositoryWrapper>();
            vehicleService = new VehicleService(mockRepository.Object);
        }

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
            ServiceDate = DateTime.Parse("2021-03-03")
        };




        [TestMethod]
        public async Task TestGetAllVehicles_ShouldContainOneVehicleInList()
        {
            //arrange
            mockRepository.Setup(x => x.VehicleRepo.GetAllVehicles()).ReturnsAsync(cars);

            //Act
            var response = await vehicleService.GetAllVehicles();

            //Assert
            Assert.AreEqual(1, response.Count());
        } 

        [TestMethod]
        public async Task TestCreateVehicle_ShouldReturnTrue()
        {
            //Arrange
            mockRepository.Setup(x => x.VehicleRepo.CreateVehicle(It.IsAny<IVehicle>())).ReturnsAsync(true);
            //Act
            var response = await vehicleService.CreateVehicle(carDTO);
            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task TestGetVehicle_ShouldReturnCorrectId()
        {
            //Arrange
            mockRepository.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);
            //Act
            var response = await vehicleService.GetVehicleById(car.Id);
            //Assert
           response.Id.Should().Be(1);
        }


        [TestMethod]
        public async Task TestGetVehicleByVehicleName_ShouldReturnCorrectRegNumber()
        {
            //Arrange
            mockRepository.Setup(x => x.VehicleRepo.GetAllVehicles()).ReturnsAsync(cars);
            //Act
            var response = await vehicleService.GetVehicleWithKeyword("ABC123");
            //Assert
        }

        [TestMethod]
        public async Task TestGetVehicleByVehicleName_ShouldReturnEmptyWhenRegNumberNotExist()
        {
            //Arrange
            mockRepository.Setup(x => x.VehicleRepo.GetAllVehicles()).ReturnsAsync(cars);
            //Act
            var response = await vehicleService.GetVehicleWithKeyword("ACC123");
            //Assert
            response.Should().BeEmpty();
        }




        [TestMethod]
        public async Task TestDeleteVehicle_ShouldReturnTrue()
        {
            //Arrange
            int randomId = 2;
            mockRepository.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);
            mockRepository.Setup(x => x.VehicleRepo.DeleteVehicle(It.IsAny<IVehicle>())).ReturnsAsync(true);
            //Act
            var response = await vehicleService.DeleteVehicle(randomId);
            //Assert
            mockRepository.Verify(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>()), Times.Once());
            mockRepository.Verify(x => x.VehicleRepo.DeleteVehicle(It.IsAny<IVehicle>()), Times.Once());
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task TestDeleteVehicle_ShouldReturnFalseWhenVehicleDoesntExist()
        {
            //Arrange
            int randomId = 2;
            mockRepository.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>()));
            //Act
            var response = await vehicleService.DeleteVehicle(randomId);
            //Assert
            mockRepository.Verify(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>()), Times.Once());
            mockRepository.Verify(x => x.VehicleRepo.DeleteVehicle(It.IsAny<IVehicle>()), Times.Never());
            response.Should().BeFalse();
        }



        [TestMethod]
        public async Task TestUpdateVehicle_ShouldReturnTrue()
        {
            //Arrange
            UpdateVehicleRequest request = new UpdateVehicleRequest()
            {
                Id = 2,
                IsDrivingBan = true,
                IsServiceBooked = false,
                ServiceDate = DateTime.Parse("2022-01-01")
            };

            mockRepository.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);
            mockRepository.Setup(x => x.VehicleRepo.UpdateVehicle(It.IsAny<IVehicle>())).ReturnsAsync(true);
            //Act
            var response = await vehicleService.UpdateVehicle(request);
            //Assert
            mockRepository.Verify(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>()), Times.Once());
            mockRepository.Verify(x => x.VehicleRepo.UpdateVehicle(It.IsAny<IVehicle>()), Times.Once());
            response.ServiceDate.Should().NotBe(DateTime.Parse("2021-01-01"));
            response.IsDrivingBan.Should().BeTrue();
        }
    }
}
