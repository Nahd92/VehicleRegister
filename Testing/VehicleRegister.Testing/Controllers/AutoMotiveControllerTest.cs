using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.CarAPI.Controllers;
using VehicleRegister.Domain.DTO.AutoMotiveDTO.Request;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Service.Interface;
using VehicleRegister.Domain.Models;

namespace VehicleRegister.Testing.Controllers
{
    [TestClass]
     public class AutoMotiveControllerTest
    {
        private readonly Mock<IServiceWrapper> mockService;
        private AutoMotiveController autoMotiveController;
        public AutoMotiveControllerTest()
        {
            mockService = new Mock<IServiceWrapper>();
            autoMotiveController = new AutoMotiveController(mockService.Object);
        }



        [TestMethod]
        public async Task GetAllAutoMotives_ShouldReturnNotFoundWhenListIsEmpty()
        {
            //Arrange
            List<IAutoMotiveRepair> emptyList = null;
            mockService.Setup(x => x.RepairService.GetAllAutoMotives()).ReturnsAsync(emptyList);

            //Act
            var response = await autoMotiveController.GetAutoMotives();
            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetAllAutoMotives_ShouldReturnOkResultWithTwoInList()
        {
            //Arrange
            IEnumerable<IAutoMotiveRepair> listOfAutoMotives = new List<IAutoMotiveRepair>()
            {
            new AutoMotiveRepair
                    {
                        Id = 1,
                    },
             new AutoMotiveRepair
                    {
                        Id = 2,
                    }
            };

            mockService.Setup(x => x.RepairService.GetAllAutoMotives()).ReturnsAsync(listOfAutoMotives);

            //Act
            var response = await autoMotiveController.GetAutoMotives();

            //Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            var automotives = result.Value.Should().BeOfType<List<IAutoMotiveRepair>>().Subject;
            automotives.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task TestAddNewAutoMotivesToDatabase_ShouldReturnNoContent()
        {
            //Arrange
            mockService.Setup(x => x.RepairService.AddNewAutoMotiveToDatabase(It.IsAny<AddAutoMotiveToListRequest>())).ReturnsAsync(true);
            //Act
            var response = await autoMotiveController.AddNewAutoMotivesToDatabase(new AddAutoMotiveToListRequest { Id = 1, Name = "Hedin bil" });
            //Assert

            response.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task TestAddNewAutoMotivesToDatabase_ShouldReturnBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.RepairService.AddNewAutoMotiveToDatabase(It.IsAny<AddAutoMotiveToListRequest>())).ReturnsAsync(false); ;
            //Act
            var response = await autoMotiveController.AddNewAutoMotivesToDatabase(new AddAutoMotiveToListRequest { Id = 1, Name = "Hedin bil" });
            //Assert

            var result = response.Should().BeOfType<BadRequestObjectResult>().Subject;
            result.Value.Should().Be("Something happened when trygin to add AutoMotive to list, try again");
        }
    }
}
