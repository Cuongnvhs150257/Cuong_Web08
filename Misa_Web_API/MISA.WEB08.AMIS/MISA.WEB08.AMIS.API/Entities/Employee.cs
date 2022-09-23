using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Nhân viên ứng với bảng employee
    /// create by: Nguyễn Văn Cương 16/8/2022
    /// </summary>
    public class Employee
    {
        //Id nhân viên
        public Guid employeeid { get; set; }

        //mã nhân viên
        public string employeecode { get; set; }

        //tên nhân viên
        public string fullname { get; set; }

        //ngày tháng năm sinh
        public DateTime dateofbirth { get; set; }

        //giới tính
        public Gender gender { get; set; }

        //chức vụ

        public string prostions { get; set; }

        //số cmnd

        public string  cmndcode { get; set; }

        //ngày cấp cmnd

        public DateTime cmnddate { get; set; }

        //nơi cấp cmnd
        public string cmndadress { get; set; }

        //địa chỉ 
        public string adress { get; set; }

        //số điện thoại
        public string phonenumber { get; set; }

        //điện thoại cố định

        public string fax { get; set; }

        //email

        public string email { get; set; }

        //tài khoản ngân hàng

        public string bankaccount { get; set; }

        //tên ngân hàng
        public string bankname { get; set; }

        //chi nhánh ngân hàng 
        public string bankunit { get; set; }
        
        //mã đơn vị 
        public Guid unitid { get; set; }
        
        //tên đơn vị 
        public string unitname { get; set; }
        
        //ngày tạo
        public DateTime createdate { get; set; }

        //người tạo
        public string createby { get; set; }

        //ngày chỉnh sửa gần nhất 
        public DateTime moditifieddate { get; set; }

        //người chỉnh sửa gần nhất
        public string moditifiedby { get; set; }
    }

}
