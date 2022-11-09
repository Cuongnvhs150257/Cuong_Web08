using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL.ProductBL
{
    /// <summary>
    /// ProductBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class ProductBL: BaseBL<Product>, IProductBL
    {
        public ProductBL(IBaseDL<Product> baseDL) : base(baseDL)
        {

        }
    }
}
