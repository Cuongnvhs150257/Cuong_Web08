using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;
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
        #region Field

        private IEmployeeDL _employeeBL;

        #endregion


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

                var employees = _employeeBL.GetAllEmployees();
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
        public IActionResult GetEmployeeByID(Guid employeeid)
        {
            
            try
            {
                var Result = _employeeBL.GetEmployeeByID(employeeid);
                return StatusCode(StatusCodes.Status200OK, Result);

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
                var numberOfAffectedRows = _employeeBL.InsertEmployee(employee);
                //Xử lý giá trị trả về
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, numberOfAffectedRows);
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
                var numberOfAffectedRows = _employeeBL.UpdateEmployee(employeeid, employee);

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
                var numberOfAffectedRows = _employeeBL.DeleteEmployee(employeeid);

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
