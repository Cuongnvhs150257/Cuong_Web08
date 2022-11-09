using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Đơn vị ứng với bảng đơn vị
    /// </summary>
    public class Unit : BaseEntities
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [PrimarKeyAttribute]
        public Guid UnitID { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Mã phòng ban không được để trống")]
        public string UnitCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Tên phòng ban không được để trống")]
        public string UnitName { get; set; }
    }
}
