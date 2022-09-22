using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Nhân viên
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

        //fax

        public string fax { get; set; }

        //email

        public string email { get; set; }

        //bankaccount

        public string bankaccount { get; set; }

        //backname
        public string bankname { get; set; }

        //bankunit  
        public string bankunit { get; set; }

        //createdate
        public DateTime createdate { get; set; }

        //createby
        public string createby { get; set; }

        //moditifieddate  
        public DateTime moditifieddate { get; set; }

        //moditifiedby
        public string moditifiedby { get; set; }
    }

}
