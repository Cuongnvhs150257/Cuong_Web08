using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class UnitController : BaseController<Unit>
    {

        #region Contructor

        /// <summary>
        /// Controller cho bảng Unit kế thừa BaseController
        /// </summary>
        /// <param name="baseBL"></param>
        public UnitController(IBaseBL<Unit> baseBL) : base(baseBL)
        {
        }

        #endregion

    }   
}
