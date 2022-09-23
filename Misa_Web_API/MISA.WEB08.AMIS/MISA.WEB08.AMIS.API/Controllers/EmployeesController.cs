using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Enums;
using MySqlConnector;
using System;

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
            //Phim tat Ctrl M O = thu gọn
            //         Ctrl M L = mở ra


            //Khởi tạo kết nối với MySQl

            try
            {

                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh MySQL
                //string getAllEmployeesCommand = "SELECT * FROM employee;";

                //khai bao ten stored produre
                string storeProdureName = "pro_selectallemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL


                //Thực hiện gọi vào DB
                var employees = mysqlConnection.Query(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
            }


            //Try catch exception



        }

        #endregion

        #region GET Employee By ID

        /// <summary>
        /// API lấy thông tin một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <returns>thông tin một nhân viên</returns>
        /// 


        [HttpGet("{employeeid}")]
        public Employee GetEmployeeByID([FromBody] Guid employeeid)
        {
            return new Employee
            {
                employeeid = employeeid,
                employeecode = "NV00001",
                fullname = "Nguyễn Văn Cương",
                dateofbirth = DateTime.Now,
                gender = Enums.Gender.Male

            };
        }

        #endregion

        #region API Filter 

        
        [HttpGet("filter")]
        public IActionResult FilterEmployees(
            [FromQuery] string? keyword,
            [FromQuery] Guid? postionID,
            [FromQuery] Guid? departmentID,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return StatusCode(StatusCodes.Status200OK, new PagingData
            {
                
            }) ;
                          
        }
        

        #endregion

        #region API POST Employee

        [HttpPost("")]

        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "pro_addnewemployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();

                var employeeID = Guid.NewGuid();
                parameters.Add("@employeeid", employeeID);
                parameters.Add("@employeecode", employee.employeecode);
                parameters.Add("@fullname", employee.fullname);
                parameters.Add("@dateofbirth", employee.dateofbirth);
                parameters.Add("@gender", employee.gender);
                parameters.Add("@positions", employee.prostions);
                parameters.Add("@@identitynumber", employee.cmndcode);
                parameters.Add("@identityplace", employee.cmndadress);
                parameters.Add("@identifydate", employee.cmnddate);
                parameters.Add("@address", employee.adress);
                parameters.Add("@phonenumber", employee.phonenumber);
                parameters.Add("@fax", employee.fax);
                parameters.Add("@email", employee.email);
                parameters.Add("@bankaccount", employee.bankaccount);
                parameters.Add("@bankname", employee.bankname);
                parameters.Add("@bankunit", employee.bankunit);
                parameters.Add("@unitid", employee.unitid);
                parameters.Add("@unitname", employee.unitname);
                parameters.Add("@createdate", DateTime.Now);
                parameters.Add("@createby", employee.createby);
                parameters.Add("@modifieddate", DateTime.Now);
                parameters.Add("@modifiedby", employee.moditifiedby);

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
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.Exception,
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
            }

        }

        #endregion

        #region API PUT Employee


        [HttpPut("{employeeid}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeid, [FromBody] Employee employee)
        {
            //Khởi tạo kết nối với MySQl

            try
            {

                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.cuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);


                //khai bao ten stored produre
                string storeProdureName = "pro_update_employee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL

                var parameters = new DynamicParameters();

                var employeeID = employee.employeeid;
                parameters.Add("@employeeid", employeeID);
                parameters.Add("@employeecode", employee.employeecode);
                parameters.Add("@fullname", employee.fullname);
                parameters.Add("@dateofbirth", employee.dateofbirth);
                parameters.Add("@gender", employee.gender);
                parameters.Add("@positions", employee.prostions);
                parameters.Add("@@identitynumber", employee.cmndcode);
                parameters.Add("@identityplace", employee.cmndadress);
                parameters.Add("@identifydate", employee.cmnddate);
                parameters.Add("@address", employee.adress);
                parameters.Add("@phonenumber", employee.phonenumber);
                parameters.Add("@fax", employee.fax);
                parameters.Add("@email", employee.email);
                parameters.Add("@bankaccount", employee.bankaccount);
                parameters.Add("@bankname", employee.bankname);
                parameters.Add("@bankunit", employee.bankunit);
                parameters.Add("@unitid", employee.unitid);
                parameters.Add("@unitname", employee.unitname);
                parameters.Add("@createdate", DateTime.Now);
                parameters.Add("@createby", employee.createby);
                parameters.Add("@modifieddate", DateTime.Now);
                parameters.Add("@modifiedby", employee.moditifiedby);

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
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.UpdateError,
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
            }


            //Try catch exception
        }

        #endregion

        #region API Detele Employee by ID

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
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AMITErrorCode.DeleteError,
                    "It was not possible to connect to the redis server",
                    "Có lỗi xảy ra, vui lòng liên hệ Misa",
                    "http",
                    HttpContext.TraceIdentifier));
            }
        }

        #endregion

        #region API Delete All Employee

        [HttpPost("batch-delete")]
        public IActionResult DeleteMultipleEmployee([FromBody] List<string> employeeid)
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        #endregion
    }
}
