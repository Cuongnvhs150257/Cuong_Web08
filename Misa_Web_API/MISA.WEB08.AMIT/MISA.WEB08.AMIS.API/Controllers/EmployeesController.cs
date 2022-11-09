using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MySqlConnector;
using System;

//Phim tat Ctrl M O = thu gọn
//         Ctrl M L = mở ra

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Controller cho bảng Employee kế thừa BaseController
    /// </summary>
    /// <param name="baseBL"></param>
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

        #region Method

        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("get-employees-excel")]
        public IActionResult GetEmployeesExcel()
        {
            try
            {
                var result = _employeeBL.GetEmployeeExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");

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


    }
}
