using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public interface IBaseBL<T>
    {
        public IEnumerable<T> GetAllRecords();
    }
}
