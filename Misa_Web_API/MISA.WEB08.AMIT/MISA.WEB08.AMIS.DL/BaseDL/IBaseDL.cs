using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Hàm kết nối DB để lấy toàn bộ nhân viên
        /// Createdby: Nguyễn Văn Cương 28/09/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Hàm kết nối DB để thêm mới đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Guid InsertRecords(T record);

        /// <summary>
        /// Hàm kết nối DB để lấy nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>
        public object GetRecordByID(Guid record);

        /// Hàm kết nối DB để sửa đối tượng theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="record"></param>
        /// <returns>numberOfAffectedRows</returns>
        public int UpdateRecord(Guid recordid, T record);

        /// <summary>
        /// Hàm kết nối DB để phân trang, tìm kiếm
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData Filter(string where, int? limit, int? offset);

        /// <summary>
        /// Hàm kết nối DB để xóa nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>
        /// <summary>
        public int DeleteRecord(Guid recordid);

        /// <summary>
        /// Hàm kết nối DB để xóa nhiều đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> DeleteMultipleRecord(List<string> recordid);

        /// <summary>
        /// Hàm kết nối DB để lấy mã đối tượng lớn nhất
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <returns></returns>
        public Object GetMaxRecord();
    }
}
