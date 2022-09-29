using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Dữ liệu trả về từ tầng BL
    /// </summary>
    public class ServiceRespone
    {
        /// <summary>
        /// Thành công khay thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Đi kèm dữ liệu thành công hay thất bại
        /// </summary>
        public object Data { get; set; }

    }
}
