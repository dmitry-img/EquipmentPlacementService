using EquipmentPlacementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!await _context.EquipmentTypes.AnyAsync())
            {
                _context.EquipmentTypes.AddRange(
                    new EquipmentType { Name = "Equipment type 1", Area = 5 },
                    new EquipmentType { Name = "Equipment type 2", Area = 4},
                    new EquipmentType { Name = "Equipment type 3", Area = 10 },
                    new EquipmentType { Name = "Equipment type 4", Area = 6 }
                    );
            }

            if (!await _context.Premisses.AnyAsync())
            {
                _context.Premisses.AddRange(
                    new Premises { Name = "Premises 1", AreaForEquipment = 50 },
                    new Premises { Name = "Premises 2", AreaForEquipment = 100 },
                    new Premises { Name = "Premises 3", AreaForEquipment = 40 },
                    new Premises { Name = "Premises 4", AreaForEquipment = 70 }
                    );
            }

            await _context.SaveChangesAsync();
        }
    }
}
