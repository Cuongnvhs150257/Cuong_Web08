

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Đơn vị tính ứng với bảng đơn vị tính
    /// create by: Nguyễn Văn Cương 16/8/2022
    /// </summary>
    public class UnitCalculate: BaseEntities
    {
        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        [PrimarKeyAttribute]
        public Guid UnitCalculateID { get; set; }

        /// <summary>
        /// Giá trị đơn vị tính
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Giá trị đơn vị tính không được để trống")][SearchNameAttribute]
        [DulicateAtrribute("Đơn vị tính <> đã tồn tại trong hệ thống, vui lòng kiểm tra lại")]
        public string UnitCalculateValue { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public int Status { get; set; }
    }
}
