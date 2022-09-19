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

        public string prostions { get; set; }

        public string  cmndcode { get; set; }

        public DateTime cmnddate { get; set; }

        public string cmndadress { get; set; }

        public string adress { get; set; }

        public int phonenumber { get; set; }


    }
}
