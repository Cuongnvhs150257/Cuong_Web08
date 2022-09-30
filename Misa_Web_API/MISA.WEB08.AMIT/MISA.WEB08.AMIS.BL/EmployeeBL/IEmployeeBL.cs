using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Hàm kết nối DL để lấy nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>
        public Employee GetEmployeeByID(Guid employeeid);


        /// <summary>
        /// Hàm kết nối DL để sửa nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="employee"></param>
        /// <returns>numberOfAffectedRows</returns>
        public int UpdateEmployee(Guid employeeid, Employee employee);

        /// <summary>
        /// Hàm kết nối DB để phân trang, tìm kiếm
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData FilterEmployees(string where, int? limit, int? offset);

        /// <summary>
        /// Hàm kết nối DL để xóa nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>

        public int DeleteEmployee(Guid employeeid);

        /// <summary>
        /// Hàm kết nối DL để xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid);
    }
}
