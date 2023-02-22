using EquipmentPlacementService.Data;
using EquipmentPlacementService.Data.Models;
using EquipmentPlacementService.Features.Premisses.Models;
using EquipmentPlacementService.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Features.Premisses
{
    public class PremisesService : IPremisesService
    {
        private ApplicationDbContext _context;

        public PremisesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string name, double areaForEquipment)
        {
            var premises = new Premises
            {
                Name = name,
                AreaForEquipment = areaForEquipment
            };

            _context.Add(premises);
            await _context.SaveChangesAsync();

            return premises.Id;
        }

        public async Task<IEnumerable<PremisesDetailsServiceModel>> GetAllAsync()
        {
            return await _context.Premisses.Select(p => new PremisesDetailsServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                AreaForEquipment = p.AreaForEquipment
            }).ToListAsync();
        }

        public async Task<double> GetPremisesFreeSpaceAsync(int premisesId)
        {
            var premises = await _context.Premisses.FirstOrDefaultAsync(p => p.Id == premisesId);

            if (premises == null)
                throw new NotFoundException(nameof(premises), premisesId);

            var contracts = _context.Contracts
                .Include(c => c.EquipmentType).Where(c => c.PremisesId == premisesId);
            var occupiedSpace = contracts.Sum(c => c.EquipmentCount * c.EquipmentType.Area);

            return premises.AreaForEquipment - occupiedSpace;
        }
    }
}
