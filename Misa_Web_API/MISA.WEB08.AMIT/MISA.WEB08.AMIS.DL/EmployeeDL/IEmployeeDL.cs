using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// interface EmployeeDL
    /// Createdby: Nguyễn Văn Cương 26/09/2022
    /// </summary>
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeExcel();
    }
}
