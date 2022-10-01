using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
       
        /// <summary>
        /// Hàm kết nối DB để sửa nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="employee"></param>
        /// <returns>numberOfAffectedRows</returns>
        public int UpdateEmployee(Guid employeeid, Employee employee)
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = String.Format(Resource.Pro_UpdateEmployee, typeof(Employee).Name);

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();

            parameters.Add("v_EmployeeID", employeeid);
            parameters.Add("v_EmployeeCode", employee.EmployeeCode);
            parameters.Add("v_FullName", employee.FullName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_Postions", employee.Postions);
            parameters.Add("v_IdentifyCode", employee.IdentifyCode);
            parameters.Add("v_IdentifyPlace", employee.IdentifyPlace);
            parameters.Add("v_IdentifyDate", employee.IdentifyDate);
            parameters.Add("v_Address", employee.Address);
            parameters.Add("v_PhoneNumber", employee.PhoneNumber);
            parameters.Add("v_Fax", employee.Fax);
            parameters.Add("v_Email", employee.Email);
            parameters.Add("v_BankAccount", employee.BankAccount);
            parameters.Add("v_BankName", employee.BankName);
            parameters.Add("v_BankUnit", employee.BankUnit);
            parameters.Add("v_UnitID", employee.UnitID);
            parameters.Add("v_UnitName", employee.UnitName);
            parameters.Add("v_CreateDate", DateTime.Now);
            parameters.Add("v_CreateBy", employee.CreateBy);
            parameters.Add("v_ModifiedDate", DateTime.Now);
            parameters.Add("v_ModifiedBy", employee.ModifiedBy);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm kết nối DB để xóa nhiều nhân viên
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid)
        {
            throw new NotImplementedException();
        }
    }
}
