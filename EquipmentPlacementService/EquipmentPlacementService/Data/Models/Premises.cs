using EquipmentPlacementService.Data.Models.Base;

namespace EquipmentPlacementService.Data.Models
{
    public class Premises : BaseEntity
    {
        public string Name { get; set; }
        public double AreaForEquipment { get; set; }
    }
}
