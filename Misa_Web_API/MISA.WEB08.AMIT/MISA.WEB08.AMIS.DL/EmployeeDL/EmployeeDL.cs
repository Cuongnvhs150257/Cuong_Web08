using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// EmployeeDl kết nối với DB
    /// Createdby: Nguyễn Văn Cương 20/09/2022
    /// </summary>

    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    { 
    }
}
