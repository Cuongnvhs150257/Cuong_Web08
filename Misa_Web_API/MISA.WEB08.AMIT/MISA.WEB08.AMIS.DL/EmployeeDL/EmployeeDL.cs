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
    public class EmployeeDL : IEmployeeDL
    {
        public IEnumerable<Employee> GetAllEmployees()
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = "Pro_employee_SelectAllEmployee";

            //Thực hiện gọi vào DB
            var employees = mysqlConnection.Query<Employee>(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);

            return employees;
        }

        public Employee GetEmployeeByID(Guid employeeid)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = "Pro_employee_SelectEmployee";

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            parameters.Add("v_EmployeeID", employeeid);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        public int InsertEmployee(Employee employee)
        {
            var properties = typeof(Employee).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var properyValue = property.GetValue(employee);
                var isNotNullOrEmptyAtrribute = (IsNotNullOrEmptyAtrribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAtrribute));
                if (isNotNullOrEmptyAtrribute != null && string.IsNullOrEmpty(properyValue?.ToString()))
                {
                    validateFailures.Add(isNotNullOrEmptyAtrribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                
            }
            //Khởi tạo kết nối với MySQl
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = "Pro_employee_InsertEmployee";

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();

            var employeeID = Guid.NewGuid();
            parameters.Add("v_EmployeeID", employeeID);
            parameters.Add("v_EmployeeCode", employee.EmployeeCode);
            parameters.Add("v_FullName", employee.FullName);
            parameters.Add("v_DateOfBirth", employee.DateOfBirth);
            parameters.Add("v_Gender", employee.Gender);
            parameters.Add("v_Postions", employee.Postions);
            parameters.Add("v_IdentifyCode", employee.IdentifyCode);
            parameters.Add("v_IdentifyPlace", employee.IdentifyPlace);
            parameters.Add("v_IdentifyDate", employee.IdentifyDate);
            parameters.Add("v_Address", employee.Address);
            parameters.Add("v_PhoneNumber", employee.Phonenumber);
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
            parameters.Add("v_ModifiedBy", employee.ModitifiedBy);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            
            return numberOfAffectedRows;

        }

        public int DeleteEmployee([FromRoute] Guid employeeid)
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
            var mysqlConnection = new MySqlConnection(connectionString);


            //khai bao ten stored produre
            string storeProdureName = "pro_deleteemployee";

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = employeeid;

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        public int UpdateEmployee(Guid employeeid, Employee employee)
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = "Pro_employee_UpdateEmployee";

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
            parameters.Add("v_PhoneNumber", employee.Phonenumber);
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
            parameters.Add("v_ModifiedBy", employee.ModitifiedBy);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        public IEnumerable<Employee> FilterEmployees(
            string keyword,
            int limit,
            int offset)
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
            var mysqlConnection = new MySqlConnection(connectionString);

            //khai bao ten stored produre
            string storeProdureName = "pro_selectallemployee";

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();

            parameters.Add("v_limit", limit);
            parameters.Add("v_offset", offset);
            parameters.Add("v_where", keyword);

            //Thực hiện gọi vào DB
            var numberOfAffectedRows = mysqlConnection.QueryMultiple(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return numberOfAffectedRows;
        }

        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid)
        {
            throw new NotImplementedException();
        }
    }
}
