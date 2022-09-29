using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MySqlConnector;
using System;

//Phim tat Ctrl M O = thu gọn
//         Ctrl M L = mở ra

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class EmployeesController : BaseController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constutor
            
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
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
                var employee = _employeeBL.GetEmployeeByID(employeeid);

                return StatusCode(StatusCodes.Status200OK, employee);

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
                var ListEmployees = _employeeBL.FilterEmployees(wnere, limit, offset);

                return StatusCode(StatusCodes.Status200OK, ListEmployees);
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
                //Xử lý giá trị trả về
                var result = _employeeBL.InsertEmployee(employee);

                if (!result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
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
