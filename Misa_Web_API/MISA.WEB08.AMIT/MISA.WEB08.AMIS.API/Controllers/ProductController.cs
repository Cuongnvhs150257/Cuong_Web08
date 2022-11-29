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

        // <summary>
        /// API thêm mới đối tượng
        /// </summary>
        /// <param name="record">đối tượng nhân viên</param>
        /// <returns>số lượng bản ghi ảnh hưởng</returns>
        /// createdby: Nguyễn Văn Cương 16/08/2022
        [HttpPost("GroupSupply")]
        public IActionResult InsertMuti([FromBody] Product record)
        {
            try
            {
                //Xử lý giá trị trả về
                var result = _productBL.InsertMutiRecords(record);

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
        /// API cập nhật danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("GroupSupply")]
        public IActionResult UpdateMutiRecords([FromQuery] Guid GroupSupply, [FromBody] Product product)
        {
            try
            {

                var result = _productBL.UpdateMutiRecords(product, GroupSupply);

                if (result.Success)
                {
                    return StatusCode(StatusCodes.Status201Created, GroupSupply);
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

        /// Lấy danh sách đối tượng
        /// </summary>
        /// <returns>Danh sách toàn bộ đối tượng</returns>
        /// Createdby: Nguyễn Văn Cương
        /// Createddate: 16/09/2022
        [HttpGet]
        [Route("getnewcode")]
        public IActionResult GetNewCode()
        {
            //Try catch exception
            try
            {
                var records = _productBL.GetNewCode();

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
    }
}
