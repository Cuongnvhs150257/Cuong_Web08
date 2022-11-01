using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class WarehouseController : BaseController<Warehouse>
    {
        #region Contructor

        /// <summary>
        /// Controller cho bảng Product kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public WarehouseController(IBaseBL<Warehouse> baseBL) : base(baseBL)
        {
        }

        # endregion
    }
}
