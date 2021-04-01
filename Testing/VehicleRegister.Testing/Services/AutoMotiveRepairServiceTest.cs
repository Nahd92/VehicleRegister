using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Business.Service;
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

        private IEnumerable<IAutoMotiveRepair> autoMotive = new List<AutoMotiveRepair>()
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
                    OrganisationNumber = 123421341,
                    Website = "wwww.hedinbil.se"
                }
            };


        [TestMethod]
        public async Task TestGetAllAutoMotives_ShouldContainOneAutoMotive()
        {
            //Arrange
            mockService.Setup(x => x.RepairRepo.GetAllAutoMotives()).ReturnsAsync(autoMotive);

            //Act
            var response = await autoMotiveService.GetAllAutoMotives();

            //Assert
            Assert.AreEqual(1, response.Count());
        }


    }
}
