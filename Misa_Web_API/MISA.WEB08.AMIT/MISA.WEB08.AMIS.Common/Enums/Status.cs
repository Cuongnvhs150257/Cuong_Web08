using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Enums
{
    public enum Status: int
    {
        /// <summary>
        /// Đang sử dụng
        /// </summary>
        ACTIVE  = 1,

        /// <summary>
        /// Ngưng sử dụng
        /// </summary>
        UNACTIVE = 2,

        /// <summary>
        /// Chưa xác định
        /// </summary>
        UNKNOW = 0,


    }
}
