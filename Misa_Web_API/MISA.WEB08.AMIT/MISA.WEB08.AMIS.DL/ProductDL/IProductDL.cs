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

        /// <summary>
        /// Hàm thêm danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDMuti"></param>
        /// <returns></returns>
        public Guid InsertMutiRecords(Product record, List<object>? RecordIDMuti);

        /// <summary>
        /// Hàm cập nhật danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDUpdateMuti"></param>
        /// <returns></returns>
        public int UpdateMutiRecords(Product record, Guid productid, List<object>? RecordIDUpdateMuti, List<object>? GroupIDMuti);

        /// <summary>
        /// Hàm cập nhật mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="number"></param>
        /// <param name="last"></param>
        public void SaveCode(string prefix, string number, string last);


        /// <summary>
        /// Hàm kết nối DB để lấy mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <returns></returns>
        public object GetNewCode();
    }
}
