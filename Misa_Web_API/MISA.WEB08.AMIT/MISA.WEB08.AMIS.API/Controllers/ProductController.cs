using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class ProductController : BaseController<Product>
    {
        #region Contructor

        /// <summary>
        /// Controller cho bảng Product kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public ProductController(IBaseBL<Product> baseBL) : base(baseBL)
        {
        }

        # endregion
    }
}
