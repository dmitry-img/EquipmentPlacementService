using EquipmentPlacementService.Data.Models.Base;

namespace EquipmentPlacementService.Data.Models
{
    public class Contract : BaseEntity
    {
        public int PremisesId { get; set; }
        public Premises Premises { get;set; }
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get;set; }
        public int EquipmentCount { get; set; }
    }
}
