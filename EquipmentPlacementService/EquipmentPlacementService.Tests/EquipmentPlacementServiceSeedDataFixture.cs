using EquipmentPlacementService.Data;
using EquipmentPlacementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Tests
{
    public class EquipmentPlacementServiceSeedDataFixture : IDisposable
    {
        public ApplicationDbContext ApplicationDbContext { get; private set; }

        public EquipmentPlacementServiceSeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MovieListDatabase")
                .Options;

            ApplicationDbContext = new ApplicationDbContext(options);

            ApplicationDbContext.EquipmentTypes.AddRange(
                    new EquipmentType { Name = "Equipment type 1", Area = 5 },
                    new EquipmentType { Name = "Equipment type 2", Area = 4 },
                    new EquipmentType { Name = "Equipment type 3", Area = 10 },
                    new EquipmentType { Name = "Equipment type 4", Area = 6 }
                    );

            ApplicationDbContext.Premisses.AddRange(
                    new Premises { Name = "Premises 1", AreaForEquipment = 50 },
                    new Premises { Name = "Premises 2", AreaForEquipment = 100 },
                    new Premises { Name = "Premises 3", AreaForEquipment = 40 },
                    new Premises { Name = "Premises 4", AreaForEquipment = 70 }
                    );

            ApplicationDbContext.SaveChanges();
        }

        public void Dispose()
        {
            ApplicationDbContext.Dispose();
        }
    }
}
