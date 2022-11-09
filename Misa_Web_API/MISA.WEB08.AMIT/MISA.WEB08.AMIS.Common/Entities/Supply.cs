
namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Nhóm vật tư hàng hóa ứng với bảng nhóm vật tư hàng hóa
    /// create by: Nguyễn Văn Cương 02/10/2022
    /// </summary>
    public class Supply: BaseEntities
    {
        /// <summary>
        /// ID nhóm vật tư hàng hóa
        /// </summary>
        [PrimarKeyAttribute]
        public Guid SupplyID { get; set; }

        /// <summary>
        /// Mã nhóm vật tư hàng hóa
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Mã nhóm vật tư, hàng hóa không được để trống")]
        public string SupplyCode { get; set; }

        /// <summary>
        /// Tên nhóm vật tư hàng hóa
        /// </summary>
        public string? SupplyName { get; set; }

        /// <summary>
        /// Trạng thái nhóm vật tư hàng hóa
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Mã nhóm vật tư hàng hóa thuộc nhóm vật tư hàng hóa
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
