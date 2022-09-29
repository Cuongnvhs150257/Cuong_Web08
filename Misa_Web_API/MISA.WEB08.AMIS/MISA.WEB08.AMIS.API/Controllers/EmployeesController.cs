using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Atrribute;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Properties;
using MySqlConnector;
using System;

//Phim tat Ctrl M O = thu gọn
//         Ctrl M L = mở ra

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region API GET Employees
        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách toàn bộ nhân viên</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022
        [HttpGet]
        [Route("")]
        public IActionResult GetAllEmployees()
        {
            //Try catch exception
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "Pro_employee_SelectAllEmployee";

                //Thực hiện gọi vào DB
                var employees = mysqlConnection.Query(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }
        }

        #endregion

        #region GET Employee By ID

        /// <summary>
        /// API lấy thông tin một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <returns>thông tin một nhân viên</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpGet("{employeeid}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeid)
        {
            
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "Pro_employee_SelectEmployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();
                parameters.Add("v_EmployeeID", employeeid);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, numberOfAffectedRows);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }

        }

        #endregion

        #region API Filter 

        [HttpGet("filter")]
        public IActionResult FilterEmployees(
            [FromQuery] string? wnere,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            try
            {
                var result = new PagingData();
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "Pro_employee_Filter";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();

                parameters.Add("v_limit", limit);
                parameters.Add("v_offset", offset);
                parameters.Add("v_where", wnere);

                //Thực hiện gọi vào DB
                var ListEmployees = mysqlConnection.QueryMultiple(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = ListEmployees.Read<Employee>().ToList();
                result.TotalCount = ListEmployees.Read<Int32>().First();

                return StatusCode(StatusCodes.Status200OK, new PagingData()
                {
                    Data = result.Data,
                    TotalCount = result.TotalCount,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }
        }


        #endregion

        #region API POST Employee

        // <summary>
        /// API thêm mới nhân viên
        /// </summary>
        /// <param name="employee">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPost("")]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                //validate dữ liệu đầu vào
                
                var properties = typeof(Employee).GetProperties();
                var validateFailures = new List<string>();
                foreach(var property in properties)
                {
                    string propertyName = property.Name;
                    var properyValue = property.GetValue(employee);
                    var isNotNullOrEmptyAtrribute = (IsNotNullOrEmptyAtrribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAtrribute));
                    if (isNotNullOrEmptyAtrribute != null && string.IsNullOrEmpty(properyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAtrribute.ErrorMessage);
                    }
                }

                if(validateFailures.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    AMITErrorCode.InsertError,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFaild,
                    Resource.MoreInfo_InsertFaild,
                    HttpContext.TraceIdentifier));
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
            
                //Xử lý giá trị trả về
                if(numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    AMITErrorCode.InsertError,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFaild,
                    Resource.MoreInfo_InsertFaild,
                    HttpContext.TraceIdentifier));
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }

        }

        #endregion

        #region API PUT Employee

        // <summary>
        /// API sửa thông tin một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <param name="employeeid">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPut("{employeeid}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeid, [FromBody] Employee employee)
        {
           

            try
            {
                //validate dữ liệu đầu vào
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
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    AMITErrorCode.InsertError,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFaild,
                    Resource.MoreInfo_InsertFaild,
                    HttpContext.TraceIdentifier));
                }

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

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeid);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    AMITErrorCode.UpdateError,
                    Resource.DevMsg_UpdateFailed,
                    Resource.UserMsg_UpdateFaild,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier));
                }

            }//Try catch exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.UpdateError,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }


            
        }

        #endregion

        #region API Detele Employee by ID

        // <summary>
        /// API xóa một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpDelete("{employeeid}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeid)
        {
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
                var mysqlConnection = new MySqlConnection(connectionString);


                //khai bao ten stored produre
                string storeProdureName = "Pro_employee_DeleteEmployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();
                parameters.Add("v_EmployeeID", employeeid);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK,employeeid);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    AMITErrorCode.DeleteError,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_Request,
                    HttpContext.TraceIdentifier));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.DeleteError,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
            }
        }

        #endregion

        #region API Delete All Employee

        // <summary>
        /// API xóa nhiều nhân viên bằng id
        /// </summary>
        /// <param name="List<string>employeeid">danh sách ID nhân viên muốn xóa</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022

        [HttpPost("batch-delete")]
        public IActionResult DeleteMultipleEmployee([FromBody] List<string> employeeid)
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        #endregion
    }
}
