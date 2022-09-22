using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;
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

                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.ctm.nvcuong;Uid=root;Pwd=012346789;";
                var mysqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh MySQL
                //string getAllEmployeesCommand = "SELECT * FROM employee;";

                //khai bao ten stored produre
                string storeProdureName = "Proc-emp";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL


                //Thực hiện gọi vào DB
                var employees = mysqlConnection.Query(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
               
                //return StatusCode(StatusCodes.Status500InternalServerError, "e001");
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
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        #endregion

        #region API PUT Employee


        [HttpPut("{employeeid}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeid, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeid);
        }

        #endregion

        #region API Detele Employee by ID

        [HttpDelete("{employeeid}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeid)
        {
            return StatusCode(StatusCodes.Status200OK, employeeid);
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
