﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
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

        #endregion

        #region API POST Record

        // <summary>
        /// API thêm mới đối tượng
        /// </summary>
        /// <param name="record">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPost("")]
        public IActionResult InsertEmployee([FromBody] T record)
        {
            try
            {
                //Xử lý giá trị trả về
                var result = _baseBL.InsertRecords(record);

                if (result.Success)
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

        #region GET Record By ID

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

        #endregion

        #region API Filter 

        /// <summary>
        /// API tìm kiếm phân trang
        /// Createdby: Nguyễn Văn Cương 16/08/2022
        /// </summary>
        /// <param name="wnere"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult FilterEmployees(
            [FromQuery] string? wnere,
            [FromQuery] int? limit,
            [FromQuery] int? offset)
        {
            try
            {
                var ListOJ = _baseBL.Filter(wnere, limit, offset);

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


        #endregion
    }
}
