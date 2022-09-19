using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <returns>Danh sách toàn bộ nhân viên</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022

        [HttpGet]
        [Route("")]
        public List<Employee> GetAllEmployees()
        {
            //Phim tat Ctrl M O = thu gọn
            //         Ctrl M L = mở ra

            return new List<Employee>
            {
                new Employee
                {
                    employeeid = Guid.NewGuid(),
                    employeecode = "NV00001",
                    fullname = "Nguyễn Văn Cương",
                    dateofbirth = DateTime.Now,
                    gender = Enums.Gender.Male,

                },
                new Employee
                {
                    employeeid = Guid.NewGuid(),
                    employeecode = "NV00001",
                    fullname = "Nguyễn Văn Cương",
                    dateofbirth = DateTime.Now,
                    gender = Enums.Gender.Male,

                }
            };
        }


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
        /*
        [HttpGet("filter")]
        public PagingData FilterEmployees(
            [FromQuery] string? keyword,
            [FromQuery] Guid? postionID,
            [FromQuery] Guid? departmentID,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            return new PagingData
            {
                Data = new List<Employee>
                {


                }
            };
        }
        */

        [HttpPost("")]

        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        [HttpPut("{employeeid}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeid, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeid);
        }

        [HttpDelete("{employeeid}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeid)
        {
            return StatusCode(StatusCodes.Status200OK, employeeid);
        }

        [HttpPost("batch-delete")]
        public IActionResult DeleteMultipleEmployee([FromBody] List<string> employeeid){
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
