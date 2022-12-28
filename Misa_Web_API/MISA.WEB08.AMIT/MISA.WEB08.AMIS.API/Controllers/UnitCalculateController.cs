using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class UnitCalculateController : BaseController<UnitCalculate>
    {
        #region Field

        private IUnitCalculateBL _unitcalculateBL;

        #endregion

        #region Contructor

        /// <summary>
        /// Controller cho bảng UnitCalculate kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public UnitCalculateController(IUnitCalculateBL unitcalculateBL) : base(unitcalculateBL)
        {
            _unitcalculateBL = unitcalculateBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách nhóm vật tư hàng hóa ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("get-unitcalculates-excel")]
        public IActionResult GetUnitCalculateExcel()
        {
            try
            {
                var result = _unitcalculateBL.GetUnitCalculateExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_don_vi_tinh.xlsx");

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
