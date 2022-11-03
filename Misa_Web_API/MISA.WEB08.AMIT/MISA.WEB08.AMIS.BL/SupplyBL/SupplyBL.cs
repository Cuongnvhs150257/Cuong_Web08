using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL.SupplyBL
{
    public class SupplyBL: BaseBL<Supply>, ISupplyBL
    {
        public SupplyBL(IBaseDL<Supply> baseDL) : base(baseDL)
        {

        }
    }
}
