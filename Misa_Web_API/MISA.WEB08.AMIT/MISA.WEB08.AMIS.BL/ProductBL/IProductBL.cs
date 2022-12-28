using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
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
    public interface IProductBL: IBaseBL<Product>
    {
        /// <summary>
        /// Hàm xuất danh sách hàng hóa ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetProductExcel();

        /// <summary>
        /// Hàm validate danh sách hàng hóa
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceRespone InsertRecord(Product record);

        /// <summary>
        /// Hàm thêm danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDMuti"></param>
        /// <returns></returns>
        public ServiceRespone InsertMutiRecords(Product record);



        /// <summary>
        /// Hàm cập nhật danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDUpdateMuti"></param>
        /// <returns></returns>
        public ServiceRespone UpdateMutiRecords(Product record, Guid productid);

        /// <summary>
        /// Hàm kết nối DB để lấy mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <returns></returns>
        public object GetNewCode();

        /// <summary>
        /// Hàm cập nhật mã tự sinh
        /// CreatedBy: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <param name="record">Bản ghi</param>
        public void SaveCode(Product record);

    }
}
