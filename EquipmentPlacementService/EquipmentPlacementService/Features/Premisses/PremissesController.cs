using EquipmentPlacementService.Features.Contracts.Models;
using EquipmentPlacementService.Features.Premisses.Models;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentPlacementService.Features.Premisses
{
    public class PremissesController : ApiController
    {
        private readonly IPremisesService _premisesService;

        public PremissesController(IPremisesService premisesService)
        {
            _premisesService = premisesService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePremisesRequestModel model)
        {
            return await _premisesService.CreateAsync(model.Name, model.AreaForEquipment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PremisesDetailsServiceModel>>> PremissesDetails()
        {
            var premises = await _premisesService.GetAllAsync();
            return Ok(premises);
        }
    }
}
