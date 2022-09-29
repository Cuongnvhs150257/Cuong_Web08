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
                var employees = _baseBL.GetAllRecords;

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
