using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;

namespace MISA.WEB08.AMIS.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;

        #endregion

        #region Contructor

            public BaseController(IBaseBL<T> baseBL)
            {
                _baseBL = baseBL;
            }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách toàn bộ đối tượng</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022
        [HttpGet]
        [Route("")]
        public IActionResult GetAllRecords()
        {
            //Try catch exception
            try
            {
                var records = _baseBL.GetAllRecords();

                return StatusCode(StatusCodes.Status200OK, records);
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

        // <summary>
        /// API thêm mới đối tượng
        /// </summary>
        /// <param name="record">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPost("")]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                //Xử lý giá trị trả về
               var result = _baseBL.InsertRecord(record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
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

        /// <summary>
        /// API lấy thông tin một đối tượng bằng id
        /// </summary>
        /// <param name="recordid">ID đối tượng</param>
        /// <returns>thông tin một đối tượng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpGet("{recordid}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordid)
        {

            try
            {
                var result = _baseBL.GetRecordByID(recordid);

                return StatusCode(StatusCodes.Status200OK, result);

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


        // <summary>
        /// API sửa thông tin một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <param name="employeeid">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPut("{recordid}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordid, [FromBody] T record)
        {
            try
            {

                var result = _baseBL.UpdateRecord(recordid, record);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, recordid);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }

            }
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

        /// <summary>
        /// API tìm kiếm phân trang
        /// Createdby: Nguyễn Văn Cương 16/08/2022
        /// </summary>
        /// <param name="wnere"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult FilterRecord(
            [FromQuery] string? wnere,
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            [FromQuery] int? soft,
            [FromQuery] string? typesoft,
            [FromQuery] string? keyword)
        {
            try
            {
                var ListOJ = _baseBL.Filter(wnere, limit, offset, soft, typesoft, keyword);

                return StatusCode(StatusCodes.Status200OK, ListOJ);
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

        // <summary>
        /// API xóa một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid">ID nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpDelete("{recordid}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordid)
        {
            try
            {
                var numberOfAffectedRows = _baseBL.DeleteRecord(recordid);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, recordid);
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

        // <summary>
        /// API xóa nhiều nhân viên bằng id
        /// </summary>
        /// <param name="List<string>employeeid">danh sách ID nhân viên muốn xóa</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022

        [HttpPost("batch-delete")]
        public IActionResult DeleteMultipleRecord([FromBody] List<Guid> ListRecordID)
        {
            try
            {
                var result = _baseBL.DeleteMultipleRecord(ListRecordID);
                return StatusCode(StatusCodes.Status200OK, result);
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

        /// <summary>
        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách toàn bộ đối tượng</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022
        [HttpGet]
        [Route("getmax")]
        public IActionResult GetMaxRecord()
        {
            //Try catch exception
            try
            {
                var records = _baseBL.GetMaxRecord();

                return StatusCode(StatusCodes.Status200OK, records);
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

        /// <summary>
        /// Hàm kiểm tra phát sinh trong khi xóa
        /// Nguyễn Văn Cương 15/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("checkdelete")]
        public IActionResult CheckDelete(Guid checkdelete)
        {

            try
            {
                var result = _baseBL.CheckDelete(checkdelete);

                return StatusCode(StatusCodes.Status200OK, result);

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
