using EquipmentPlacementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Premises> Premisses { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}
