using EquipmentPlacementService.Data.Models;

namespace EquipmentPlacementService.Features.Contracts.Models
{
    public class CreateContractRequestModel
    {
        public int PremisesId { get; set; }
        public int EquipmentTypeId { get; set; }
        public int EquipmentCount { get; set; }
    }
}
