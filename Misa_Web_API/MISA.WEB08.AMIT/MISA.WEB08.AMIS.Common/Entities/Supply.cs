
namespace MISA.WEB08.AMIS.Common.Entities
{
    public class Supply: BaseEntities
    {
        [PrimarKeyAttribute]
        public Guid SupplyID { get; set; }

        [IsNotNullOrEmptyAtrribute("Tên nhóm vật tư, hàng hóa không được để trống")]
        public string SupplyCode { get; set; }

        public string? SupplyName { get; set; }

        public int? Status { get; set; }
    }
}
