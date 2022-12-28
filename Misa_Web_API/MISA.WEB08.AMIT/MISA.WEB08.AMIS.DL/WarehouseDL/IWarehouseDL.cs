using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// interface kết nối với Database
    /// Createdby: Nguyễn Văn Cương 02/10/2022
    /// </summary>
    public interface IWarehouseDL: IBaseDL<Warehouse>
    {
        /// <summary>
        /// Hàm xuất danh sách kho ra Excel
        /// Createby: Nguyễn Văn Cương 15/11/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Warehouse> GetWarehouseExcel();
    }
}
