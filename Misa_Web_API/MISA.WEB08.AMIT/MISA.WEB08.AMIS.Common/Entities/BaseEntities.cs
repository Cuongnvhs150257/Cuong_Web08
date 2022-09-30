using MISA.WEB08.AMIS.Common.Resource;
namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Enities dùng chung
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    public class BaseEntities
    {
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreateBy { get; set; } = "Admin";

        /// <summary>
        /// ngày chỉnh sửa gần nhất 
        /// </summary>
        public DateTime ModitifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// người chỉnh sửa gần nhất
        /// </summary>
        public string ModitifiedBy { get; set; } = "Admin";
    }
}
