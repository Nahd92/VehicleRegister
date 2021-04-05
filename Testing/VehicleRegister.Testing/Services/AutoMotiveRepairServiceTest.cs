using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Response;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Testing.Services
{
    [TestClass]
    public class AutoMotiveRepairServiceTest
    {
        private readonly Mock<IRepositoryWrapper> mockService;
        private AutoMotiveRepairService autoMotiveService;
        public AutoMotiveRepairServiceTest()
        {
            mockService = new Mock<IRepositoryWrapper>();
            autoMotiveService = new AutoMotiveRepairService(mockService.Object);
        }

        private IEnumerable<IAutoMotiveRepair> autoMotives = new List<AutoMotiveRepair>()
            {
                new AutoMotiveRepair()
                {
                    Id = 1,
                    Name = "Hedin bil",
                    Address = "Gothenburg",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Description = "Very good automotive, and kind staff",
                    PhoneNumber = 0543-20123,
                    OrganisationNumber = "ABC123",
                    Website = "wwww.hedinbil.se"
                }
            };

        private IAutoMotiveRepair autoMotive = new AutoMotiveRepair()
            {
                    Id = 1,
                    Name = "Hedin bil",
                    Address = "Gothenburg",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Description = "Very good automotive, and kind staff",
                    PhoneNumber = 0543-20123,
                    OrganisationNumber = "213123123",
                    Website = "wwww.hedinbil.se"
                
            };


        [TestMethod]
        public async Task TestGetAllAutoMotives_ShouldContainOneAutoMotive()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.GetAllAutoMotives()).ReturnsAsync(autoMotives);

            //Act
            var response = await autoMotiveService.GetAllAutoMotives();

            //Assert
            Assert.AreEqual(1, response.Count());
        }

        [TestMethod]
        public async Task TestGetAutoMotiveById_ShouldReturnOne()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.GetAutoMotive(It.IsAny<int>())).ReturnsAsync(autoMotive);
            //Act
            var response = await autoMotiveService.GetAutoMotiveById(autoMotive.Id);
            //Assert
            response.Id.Should().Be(1);
            response.Name.Should().Be("Hedin bil");
        }

        [TestMethod]
        public async Task TestGetAutoMotiveById_ShouldReturnNullWhenAutoMotiveNotExist()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.GetAutoMotive(It.IsAny<int>()));
            //Act
            var response = await autoMotiveService.GetAutoMotiveById(autoMotive.Id);
            //Assert
            response.Should().BeNull();
        }

        [TestMethod]
        public async Task TestAddNewAutoMotiveToDatabase_ShouldReturnTrue()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.CreateNewAutoMotive(It.IsAny<IAutoMotiveRepair>())).ReturnsAsync(true);
            //Act
            var response = await autoMotiveService.AddNewAutoMotiveToDatabase(new AddAutoMotiveToListRequest
            {
                Name = "Hedin bil",
                Address = "Gothenburg",
                City = "Gothenburg",
                Country = "Sweden",
                Description = "Very good automotive, and kind staff",
                PhoneNumber = 054120123,
                Website = "wwww.hedinbil.se",
            });
            //Assert
            response.Should().BeTrue();
        }

        [TestMethod]
        public async Task TestOrganisationNumberAlreadyExist_ShouldReturnTrue()
        {
            //Arrange
            string regNumber = "ABC123";
            mockService.Setup(x => x.RepairRepo.GetAllAutoMotives()).ReturnsAsync(autoMotives);
            //Act
            var response = await autoMotiveService.OrgansationNumberAlreadyExist
                (regNumber);
            //Assert
            response.Should().BeTrue();
        }
        [TestMethod]
        public async Task TestOrganisationNumberAlreadyExist_ShouldReturnFalse()
        {
            //Arrange
            string regNumber = "ACC123";
            mockService.Setup(x => x.RepairRepo.GetAllAutoMotives()).ReturnsAsync(autoMotives);
            //Act
            var response = await autoMotiveService.OrgansationNumberAlreadyExist
                (regNumber);
            //Assert
            response.Should().BeFalse();
        }
        
        [TestMethod]
        public async Task TestUpdateAutoMotive_ShouldReturnUpdatedObject()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.GetAutoMotive(It.IsAny<int>())).ReturnsAsync(autoMotive);
            mockService.Setup(x => x.RepairRepo.UpdateAutMotive(It.IsAny<IAutoMotiveRepair>())).ReturnsAsync(true);
            //Act
            var response = await autoMotiveService.UpdateAutoMotive(new UpdateAutoMotive { Id = 1, Name = "HedinBil", City = "Halmstad" });
            //Assert
            autoMotive.City.Should().Be("Halmstad");
        }
    }
}
