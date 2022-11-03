using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common.Entities;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class SupplyController : BaseController<Supply>
    {
        #region Contructor

        /// <summary>
        /// Controller cho bảng Product kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public SupplyController(IBaseBL<Supply> baseBL) : base(baseBL)
        {
        }

        # endregion
    }
}
