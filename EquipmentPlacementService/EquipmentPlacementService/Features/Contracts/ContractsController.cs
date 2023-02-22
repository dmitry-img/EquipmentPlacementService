using EquipmentPlacementService.Features.Contracts.Models;
using EquipmentPlacementService.Features.Premisses;
using EquipmentPlacementService.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentPlacementService.Features.Contracts
{
    public class ContractsController : ApiController
    {
        private readonly IContractService _contractService;
        private readonly IPremisesService _premisesService;

        public ContractsController(IContractService contractService, IPremisesService premisesService)
        {
            _contractService = contractService;
            _premisesService = premisesService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateContractRequestModel model)
        {
            var spaceToAdd = _contractService
                .GetContractEquipmentSpaceToAdd(model.EquipmentTypeId, model.EquipmentCount);
            
            var freeSpace = await _premisesService.GetPremisesFreeSpaceAsync(model.PremisesId);

            if (spaceToAdd > freeSpace)
                throw new NotEnoughSpaceException(model.PremisesId);

            var id = await _contractService
                .CreateAsync(model.PremisesId, model.EquipmentTypeId, model.EquipmentCount);

            return Created(nameof(Created), id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDetailsServiceModel>>> ContractsDetails()
        {
            var contracts = await _contractService.GetAllAsync();
            return Ok(contracts);
        }
    }
}
