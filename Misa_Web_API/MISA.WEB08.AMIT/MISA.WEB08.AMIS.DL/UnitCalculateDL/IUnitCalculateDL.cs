using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// interface lấy dữ liệu bảng đơn vị tính
    /// Createdby: Nguyễn Văn Cương 02/10/2022
    /// </summary>
    public interface IUnitCalculateDL: IBaseDL<UnitCalculate>
    {
        /// <summary>
        /// Hàm xuất danh sách đơn vị tính ra Excel
        /// Createby: Nguyễn Văn Cương 15/11/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UnitCalculate> GetUnitCalculateExcel();
    }
}
