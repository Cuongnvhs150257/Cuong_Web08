using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL.UnitCalculateBL
{
    /// <summary>
    /// UnitCalculateBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class UnitCalculateBL: BaseBL<UnitCalculate>, IUnitCalculateBL
    {
        public UnitCalculateBL(IBaseDL<UnitCalculate> baseDL) : base(baseDL)
        {

        }
    }
}
