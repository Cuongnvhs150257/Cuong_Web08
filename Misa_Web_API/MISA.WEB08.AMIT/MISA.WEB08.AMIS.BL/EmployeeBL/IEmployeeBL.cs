using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    
    /// <summary>
    /// hàm kết nối dl lấy dữ liệu
    /// Createdby: Nguyễn Văn Cương 26/9/2022
    /// </summary>
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetEmployeeExcel();

    }
}
