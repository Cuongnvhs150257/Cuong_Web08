using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// EmployeeDl kết nối với DB
    /// Createdby: Nguyễn Văn Cương 20/09/2022
    /// </summary>

    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetEmployeeExcel()
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;

            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên stored procedure 
                string storedProcedureName = String.Format(Resource.Proc_SelectAllRecord, typeof(Employee).Name);

                // Thực hiện gọi vào DB để chạy procedure
                var employees = mysqlConnection.Query<Employee>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                return employees;
            }
        }
    }
}
