using EquipmentPlacementService.Data;
using EquipmentPlacementService.Features.Contracts;
using EquipmentPlacementService.Features.Contracts.Models;
using EquipmentPlacementService.Features.Premisses;
using EquipmentPlacementService.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EquipmentPlacementService.Tests.Features.Contracts
{
    public class ContractsControllerTests
    {
        private readonly ContractsController _contractsController;
        private readonly ApplicationDbContext _context;
        private readonly IContractService _contractService;
        private readonly IPremisesService _premisesService;


        public ContractsControllerTests()
        {
            var fixture = new EquipmentPlacementServiceSeedDataFixture();
            _context = fixture.ApplicationDbContext;
            _contractService = new ContractService(_context);
            _premisesService = new PremisesService(_context);
            _contractsController = new ContractsController(_contractService, _premisesService);
        }

        [Test]
        public async Task ShouldThrowExceptionWhenNotEnoughSpace()
        {
            //Arrange
            var requestModel = new CreateContractRequestModel
            {
                PremisesId = 1,
                EquipmentTypeId = 3,
                EquipmentCount = 100
            };

            // Act and Assert
            Assert.ThrowsAsync<NotEnoughSpaceException>( async () => 
            await _contractsController.Create(requestModel));
        }

        [Test]
        public async Task ShouldReturnNewlyCreatedEntityId()
        {
            //Arrange
            var requestModel = new CreateContractRequestModel
            {
                PremisesId = 1,
                EquipmentTypeId = 3,
                EquipmentCount = 1
            };

            // Act
            var id = await _contractsController.Create(requestModel);

            //Assert
            Assert.That(id.Value, Is.EqualTo(0));
        }

        [Test]
        public async Task ShouldReturnContractsList()
        {
            //Arrange
            await _contractService.CreateAsync(1, 3, 1);

            //Act
            var list = await _contractsController.ContractsDetails();
            var actual = (list.Result as OkObjectResult).Value as IEnumerable<ContractDetailsServiceModel>;

            //Assert
            Assert.That(actual.Count(), Is.EqualTo(1));

        }
    }
}
