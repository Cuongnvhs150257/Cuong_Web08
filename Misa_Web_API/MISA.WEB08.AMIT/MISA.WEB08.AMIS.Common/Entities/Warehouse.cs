using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.Common.Entities
{
    public class Warehouse : BaseEntities
    {
        [PrimarKeyAttribute]
        public Guid WarehouseID { get; set; }

        public string WarehouseCode { get; set; }

        public string WarehouseName { get; set; }

        public string WarehouseAccount { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public string Branch { get; set; }
    }
}
