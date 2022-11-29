using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Controller cho bảng Product kế thừa BaseController
    /// </summary>
    /// <param name="baseBL"></param>
    public class SupplyController : BaseController<Supply>
    {
        #region Field

        private ISupplyBL _supplyBL;

        #endregion

        #region Contructor

        /// <summary>
        /// Controller cho bảng Supply kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public SupplyController(ISupplyBL supplyBL) : base(supplyBL)
        {
            _supplyBL = supplyBL;
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
        [Route("get-supplys-excel")]
        public IActionResult GetSupplyExcel()
        {
            try
            {
                var result = _supplyBL.GetSupplyExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhom_vat_tu_hang_hoa_dich_vu.xlsx");

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
