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

    }
}
