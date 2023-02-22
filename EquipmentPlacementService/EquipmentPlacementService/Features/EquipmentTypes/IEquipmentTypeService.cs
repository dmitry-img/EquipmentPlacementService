using EquipmentPlacementService.Features.EquipmentTypes.Models;

namespace EquipmentPlacementService.Features.EquipmentTypes
{
    public interface IEquipmentTypeService
    {
        Task<IEnumerable<EquipmentTypeDetailsServiceModel>> GetAllAsync();
        Task<int> CreateAsync(string name, double area);
    }
}
