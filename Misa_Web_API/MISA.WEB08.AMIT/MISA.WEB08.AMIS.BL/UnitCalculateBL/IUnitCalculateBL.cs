﻿using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// interface kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public interface IUnitCalculateBL: IBaseBL<UnitCalculate>
    {
        /// <summary>
        /// Hàm xuất danh sách đơn vị tính ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetUnitCalculateExcel();
    }
}
