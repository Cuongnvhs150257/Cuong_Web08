using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.API.Controllers
{
    public class UnitController : BaseController<Unit>
    {

        #region Contructor

        public UnitController(IBaseBL<Unit> baseBL) : base(baseBL)
        {
        }

        #endregion

    }   
}
