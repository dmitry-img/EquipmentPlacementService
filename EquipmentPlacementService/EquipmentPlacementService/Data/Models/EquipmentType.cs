using EquipmentPlacementService.Data.Models.Base;

namespace EquipmentPlacementService.Data.Models
{
    public class EquipmentType : BaseEntity
    {
        public string Name { get; set; }
        public double Area { get; set; }
    }
}
