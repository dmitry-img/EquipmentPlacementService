using EquipmentPlacementService.Data.Models;
using EquipmentPlacementService.Features.Premisses.Models;

namespace EquipmentPlacementService.Features.Premisses
{
    public interface IPremisesService
    {
        Task<IEnumerable<PremisesDetailsServiceModel>> GetAllAsync();
        Task<int> CreateAsync(string name, double areaForEquipment);
        Task<double> GetPremisesFreeSpaceAsync(int premisesId);
    }
}
