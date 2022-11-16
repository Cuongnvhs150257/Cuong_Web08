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
    public class ProductController : BaseController<Product>
    {
        #region Field

        private IProductBL _productBL;

        #endregion

        #region Contructor

        /// <summary>
        /// Controller cho bảng Product kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public ProductController(IProductBL productBL) : base(productBL)
        {
            _productBL = productBL;
        }

        # endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách hàng hóa ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("get-products-excel")]
        public IActionResult GetProductExcel()
        {
            try
            {
                var result = _productBL.GetProductExcel();
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_hang_hoa_dich_vu.xlsx");

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
