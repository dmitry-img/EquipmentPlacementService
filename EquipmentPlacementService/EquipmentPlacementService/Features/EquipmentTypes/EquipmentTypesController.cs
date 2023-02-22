using EquipmentPlacementService.Features.Premisses.Models;
using Microsoft.AspNetCore.Mvc;
using EquipmentPlacementService.Features.EquipmentTypes.Models;

namespace EquipmentPlacementService.Features.EquipmentTypes
{
    public class EquipmentTypesController : ApiController
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypesController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePremisesRequestModel model)
        {
            return await _equipmentTypeService.CreateAsync(model.Name, model.AreaForEquipment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentTypeDetailsServiceModel>>> EquipmentTypesDetails()
        {
            var equipmentTypes = await _equipmentTypeService.GetAllAsync();
            return Ok(equipmentTypes);
        }
    }
}
