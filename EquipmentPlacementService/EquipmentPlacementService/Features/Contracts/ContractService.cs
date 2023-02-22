using EquipmentPlacementService.Data;
using EquipmentPlacementService.Data.Models;
using EquipmentPlacementService.Features.Contracts.Models;
using EquipmentPlacementService.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EquipmentPlacementService.Features.Contracts
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _context;

        public ContractService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(int premisesId, int equipmentTypeId, int equipmentCount)
        {
            var contract = new Contract
            {
                PremisesId = premisesId,
                EquipmentTypeId = equipmentTypeId,
                EquipmentCount = equipmentCount
            };

            _context.Add(contract);
            await _context.SaveChangesAsync();

            return contract.Id;
        }

        public async Task<IEnumerable<ContractDetailsServiceModel>> GetAllAsync()
        {
            return await _context.Contracts
                .Include(p => p.Premises)
                .Include(p => p.EquipmentType)
                .Select(c => new ContractDetailsServiceModel
                    {
                        PremisesName = c.Premises.Name,
                        EquipmentTypeName = c.EquipmentType.Name,
                        EquipmentCount = c.EquipmentCount
                    }).ToListAsync();
        }

        public double GetContractEquipmentSpaceToAdd(int equipmentTypeId, int equipmentCount)
        {
            var equipmentType = _context.EquipmentTypes.FirstOrDefault(e => e.Id == equipmentTypeId);

            if(equipmentType == null)
                throw new NotFoundException(nameof(equipmentType), equipmentTypeId);
            
            return equipmentType.Area * equipmentCount;
        }
    }
}
