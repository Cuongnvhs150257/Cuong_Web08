using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Nhân viên ứng với bảng employee
    /// create by: Nguyễn Văn Cương 16/8/2022
    /// </summary>
    public class Employee : BaseEntities
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [PrimarKeyAttribute]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Mã nhân viên không được để trống")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Tên nhân viên không được để trống")]
        public string FullName { get; set; }

        /// <summary>
        /// ngày tháng năm sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// chức vụ
        /// </summary>
        public string Postions { get; set; }

        /// <summary>
        /// số cmnd
        /// </summary>
        public string IdentifyCode { get; set; }

        /// <summary>
        /// ngày cấp cmnd
        /// </summary>
        public DateTime? IdentifyDate { get; set; }

        /// <summary>
        /// nơi cấp cmnd
        /// </summary>
        public string IdentifyPlace { get; set; }

        /// <summary>
        /// địa chỉ 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// điện thoại cố định
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng 
        /// </summary>
        public string BankUnit { get; set; }

        /// <summary>
        /// mã đơn vị 
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Mã đơn vị không được để trống")]
        public Guid UnitID { get; set; }

        /// <summary>
        /// tên đơn vị 
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        
    }

}
