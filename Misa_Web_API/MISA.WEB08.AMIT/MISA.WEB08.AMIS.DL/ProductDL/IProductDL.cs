using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// interface lấy dữ liệu bảng hàng hóa
    /// Createby: Nguyễn Văn Cương 28/09/2022
    /// </summary>
    public interface IProductDL: IBaseDL<Product>
    {
        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProductExcel();
    }
}
