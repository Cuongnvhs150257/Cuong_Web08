using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common
{
    public class BaseEntities
    {
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// ngày chỉnh sửa gần nhất 
        /// </summary>
        public DateTime ModitifiedDate { get; set; }

        /// <summary>
        /// người chỉnh sửa gần nhất
        /// </summary>
        public string ModitifiedBy { get; set; }
    }
}
