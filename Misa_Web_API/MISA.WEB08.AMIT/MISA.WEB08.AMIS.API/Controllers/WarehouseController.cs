using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class WarehouseController : BaseController<Warehouse>
    {
        #region Field

        private IWarehouseBL _warehouseBL;

        #endregion
        #region Contructor

        /// <summary>
        /// Controller cho bảng Warehouse kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public WarehouseController(IWarehouseBL warehouseBL) : base(warehouseBL)
        {
            _warehouseBL = warehouseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách kho ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("get-warehouses-excel")]
        public IActionResult GetUnitCalculateExcel()
        {
            try
            {
                var result = _warehouseBL.GetWarehouseExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_kho.xlsx");

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
