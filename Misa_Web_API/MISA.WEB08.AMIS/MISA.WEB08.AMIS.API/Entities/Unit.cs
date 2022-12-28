using MISA.WEB08.AMIS.API.Atrribute;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Đơn vị ứng với bảng employee
    /// create by: Nguyễn Văn Cương 16/8/2022
    /// </summary>
    public class Unit 
    {
        /// <summary>
        /// ID Đơn vị
        /// </summary>
        [PrimarKeyAttribute]
        public Guid UnitID { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// ngày chỉnh sửa gần nhất 
        /// </summary>
        public DateTime ModitifiedDate { get; set; }

        /// <summary>
        /// người chỉnh sửa gần nhất
        /// </summary>
        public string ModitifiedBy { get; set; }
    }
}
