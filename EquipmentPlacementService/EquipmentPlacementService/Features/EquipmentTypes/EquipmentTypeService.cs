using EquipmentPlacementService.Data;
using EquipmentPlacementService.Data.Models;
using EquipmentPlacementService.Features.EquipmentTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Features.EquipmentTypes
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly ApplicationDbContext _context;

        public EquipmentTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string name, double area)
        {
            var equipmentType = new EquipmentType
            {
                Name = name,
                Area = area,
            };

            _context.Add(equipmentType);
            await _context.SaveChangesAsync();

            return equipmentType.Id;
        }

        public async Task<IEnumerable<EquipmentTypeDetailsServiceModel>> GetAllAsync()
        {
            return await _context.EquipmentTypes.Select(e => new EquipmentTypeDetailsServiceModel
            {
                Id = e.Id,
                Name = e.Name,
                Area = e.Area,
            }).ToListAsync();
        }
    }
}
