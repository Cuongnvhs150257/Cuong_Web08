using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Enums
{
    public enum TaxReduction: int
    {
        /// <summary>
        /// Không được giảm thuế
        /// </summary>
        UNREDUCTION = 0,

        /// <summary>
        /// Được giảm thuế
        /// </summary>
        REDUCTION = 1,

        /// <summary>
        /// Chưa xác định
        /// </summary>
        UNKNOW = 2,
    }
}
