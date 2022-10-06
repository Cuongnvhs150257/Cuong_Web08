using MISA.WEB08.AMIS.Common;
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
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreateBy { get; set; } = Resource.Default_User; 

        /// <summary>
        /// ngày chỉnh sửa gần nhất 
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// người chỉnh sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; } = Resource.Default_User;
    }
}
