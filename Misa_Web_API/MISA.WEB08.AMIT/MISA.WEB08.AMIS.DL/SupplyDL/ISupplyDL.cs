﻿using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// interface lấy dữ liệu bảng nhóm vật tư hàng hóa
    /// Createdby: Nguyễn Văn Cương 02/10/2022
    /// </summary>
    public interface ISupplyDL: IBaseDL<Supply>
    {
        /// <summary>
        /// Hàm xuất danh sách ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Supply> GetSupplyExcel();

    }
}
