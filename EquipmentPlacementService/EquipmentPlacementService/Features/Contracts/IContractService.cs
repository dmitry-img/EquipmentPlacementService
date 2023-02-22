using EquipmentPlacementService.Features.Contracts.Models;

namespace EquipmentPlacementService.Features.Contracts
{
    public interface IContractService
    {
        public Task<int> CreateAsync(int premisesId, int equipmentTypeId, int equipmentCount);
        public Task<IEnumerable<ContractDetailsServiceModel>> GetAllAsync();
        public double GetContractEquipmentSpaceToAdd(int equipmentTypeId, int equipmentCount);
    }
}
