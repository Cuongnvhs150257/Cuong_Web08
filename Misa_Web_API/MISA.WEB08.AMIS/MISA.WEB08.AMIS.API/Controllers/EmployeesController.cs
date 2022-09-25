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
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;UID=root;Password=012346789";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_selectallemployee";

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
        public IActionResult GetEmployeeByID([FromBody] Guid employeeid)
        {
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_selectallemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = employeeid;

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, employeeid);

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
            [FromQuery] string? keyword,
            [FromQuery] string? phonenumber,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_selectallemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();

                parameters.Add("v_limit", limit);
                parameters.Add("v_offset", offset);
                parameters.Add("v_where", keyword);
                parameters.Add("v_phonenumber", phonenumber);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.QueryMultiple(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK);
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
                    var isNotNullOrEmptyAtrribute = (IsNotNullOrEmptyAtrribute?)Atrribute.PrimarKeyAttribute(property, typeof(IsNotNullOrEmptyAtrribute));
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
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_addnewemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();

                var employeeID = Guid.NewGuid();
                parameters.Add("@employeeid", employeeID);
                parameters.Add("@employeecode", employee.EmployeeCode);
                parameters.Add("@fullname", employee.FullName);
                parameters.Add("@dateofbirth", employee.DateOfBirth);
                parameters.Add("@gender", employee.Gender);
                parameters.Add("@positions", employee.Postions);
                parameters.Add("@identitycode", employee.IndentifyCode);
                parameters.Add("@identityplace", employee.IndentifyAddress);
                parameters.Add("@identifydate", employee.IndentifyDate);
                parameters.Add("@address", employee.Address);
                parameters.Add("@phonenumber", employee.Phonenumber);
                parameters.Add("@fax", employee.Fax);
                parameters.Add("@email", employee.Email);
                parameters.Add("@bankaccount", employee.BankAccount);
                parameters.Add("@bankname", employee.BankName);
                parameters.Add("@bankunit", employee.BankUnit);
                parameters.Add("@unitid", employee.UnitID);
                parameters.Add("@unitname", employee.UnitName);
                parameters.Add("@createdate", DateTime.Now);
                parameters.Add("@createby", employee.CreateBy);
                parameters.Add("@modifieddate", DateTime.Now);
                parameters.Add("@modifiedby", employee.ModitifiedBy);

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
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_update_employee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();

                var employeeID = employee.EmployeeID;
                parameters.Add("@employeeid", employeeID);
                parameters.Add("@employeeid", employeeID);
                parameters.Add("@employeecode", employee.EmployeeCode);
                parameters.Add("@fullname", employee.FullName);
                parameters.Add("@dateofbirth", employee.DateOfBirth);
                parameters.Add("@gender", employee.Gender);
                parameters.Add("@positions", employee.Postions);
                parameters.Add("@identitycode", employee.IndentifyCode);
                parameters.Add("@identityplace", employee.IndentifyAddress);
                parameters.Add("@identifydate", employee.IndentifyDate);
                parameters.Add("@address", employee.Address);
                parameters.Add("@phonenumber", employee.Phonenumber);
                parameters.Add("@fax", employee.Fax);
                parameters.Add("@email", employee.Email);
                parameters.Add("@bankaccount", employee.BankAccount);
                parameters.Add("@bankname", employee.BankName);
                parameters.Add("@bankunit", employee.BankUnit);
                parameters.Add("@unitid", employee.UnitID);
                parameters.Add("@unitname", employee.UnitName);
                parameters.Add("@createdate", DateTime.Now);
                parameters.Add("@createby", employee.CreateBy);
                parameters.Add("@modifieddate", DateTime.Now);
                parameters.Add("@modifiedby", employee.ModitifiedBy);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeID);
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
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);


                //khai bao ten stored produre
                string storeProdureName = "pro_deleteemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = employeeid;

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
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
