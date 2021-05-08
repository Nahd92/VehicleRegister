using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VehicleRegister.Business.Service;
using VehicleRegister.Domain.DTO.ReservationsDTO.Request;
using VehicleRegister.Domain.Interfaces.Extensions.Interface;
using VehicleRegister.Domain.Interfaces.Model.Interface;
using VehicleRegister.Domain.Interfaces.Repository.Interface;
using VehicleRegister.Domain.Models;
using VehicleRegister.Domain.Models.Vehicles;

namespace VehicleRegister.Testing.API.Services
{
    [TestClass]
    public class ServiceReservationTest
    {
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly ServiceReservationsService serviceReservations;
        private readonly Mock<ISpecialLoggerExtension> mockLogger;
        public ServiceReservationTest()
        {
            mockRepository = new Mock<IRepositoryWrapper>();
            mockLogger = new Mock<ISpecialLoggerExtension>();
            serviceReservations = new ServiceReservationsService(mockRepository.Object, mockLogger.Object);
        }


        private CreateReservationRequest createReservation = new CreateReservationRequest
        {
            VehicleId = 1,
            Date = DateTime.Now,
            AutoMotiveRepairId = 2
        };
        IVehicle car = new Vehicle { Id = 1 };
        IAutoMotiveRepair autoMotive = new AutoMotiveRepair { Id = 1 };


        [TestMethod]
        public async Task BookService_ShouldReturnTrue()
        {
            //Arrange
            mockLogger.Setup(x => x.GetActualAsyncMethodName(It.IsAny<string>()));
            mockRepository.Setup(x => x.VehicleRepo.GetVehicleById(It.IsAny<int>())).ReturnsAsync(car);
            mockRepository.Setup(x => x.RepairRepo.GetAutoMotive(It.IsAny<int>())).ReturnsAsync(autoMotive);
            mockRepository.Setup(x => x.ServiceRepo.CreateReservations(It.IsAny<IServiceReservations>())).ReturnsAsync(true);
            mockRepository.Setup(x => x.VehicleRepo.UpdateVehicle(It.IsAny<IVehicle>())).ReturnsAsync(true);
            //Act

            var response = await serviceReservations.BookService(createReservation);

            //Assert
            response.Should().BeTrue();

        }
    }
}