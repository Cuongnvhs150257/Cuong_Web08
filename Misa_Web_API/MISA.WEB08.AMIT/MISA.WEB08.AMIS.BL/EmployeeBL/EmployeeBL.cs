using Microsoft.AspNetCore.Http;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion


        #region Method
        

        public Employee GetEmployeeByID(Guid employeeid)
        {
            return _employeeDL.GetEmployeeByID(employeeid);
        }

        public int UpdateEmployee(Guid employeeid, Employee employee)
        {
            return _employeeDL.UpdateEmployee(employeeid, employee);
        }

        public int DeleteEmployee(Guid employeeid)
        {
            return _employeeDL.DeleteEmployee(employeeid);
        }

        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid)
        {
            return _employeeDL.DeleteMultipleEmployee(employeeid);
        }

        public PagingData FilterEmployees(string keyword, int? limit, int? offset)
        {
            return _employeeDL.FilterEmployees(keyword, limit, offset);
        }


        #endregion
    }

}
