using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Kho ứng với bảng kho
    /// </summary>
    public class Warehouse : BaseEntities
    {
        /// <summary>
        /// ID kho
        /// </summary>
        [PrimarKeyAttribute]
        public Guid WarehouseID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [SearchCodeAttribute]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [SearchNameAttribute]
        public string WarehouseName { get; set; }

        /// <summary>
        /// Tài khoản khi
        /// </summary>
        public string WarehouseAccount { get; set; }

        /// <summary>
        /// Địa chỉ kho
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Chi nhánh kho
        /// </summary>
        public string Branch { get; set; }
    }
}
