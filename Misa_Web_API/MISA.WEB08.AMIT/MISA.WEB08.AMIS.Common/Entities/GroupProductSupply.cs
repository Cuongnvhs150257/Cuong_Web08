using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    public class GroupProductSupply
    {
        /// <summary>
        /// Id bảng trung gian nhóm vật tư hàng hóa, danh sách hàng hóa
        /// </summary>
        [PrimarKeyAttribute]
        public Guid GroupID { get; set; }

        /// <summary>
        /// ID danh sách hàng hóa
        /// </summary>
        [ProductAttribute]
        public Guid? ProductID { get; set; }
        
        /// <summary>
        /// ID nhóm vật tư hàng hóa
        /// </summary>
        [ListIDAttribute]
        public Guid? SupplyID { get; set; }
    }
}
