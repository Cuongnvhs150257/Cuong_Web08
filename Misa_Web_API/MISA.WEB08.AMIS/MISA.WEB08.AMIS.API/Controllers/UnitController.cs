using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.API.Entities;
using MISA.WEB08.AMIS.API.Enums;
using MISA.WEB08.AMIS.API.Properties;
using MySqlConnector;

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        #region API GET Unit

        /// <summary>
        /// Lấy danh sách Đơn vị
        /// </summary>
        /// <returns>Danh sách toàn bộ Đơn vị</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022
        [HttpGet]
        [Route("")]
        public IActionResult GetAllUnit()
        {
            //Try catch exception
            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost;Port=3306;Database=misa.web08.cmt.cuong;Uid=root;Pwd=123456789";
                var mysqlConnection = new MySqlConnection(connectionString);

                //khai bao ten stored produre
                string storeProdureName = "Pro_unit_GetAll";

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
    }
}
