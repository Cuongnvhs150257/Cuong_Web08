using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Contructor

        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion


        #region Method
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeDL.GetAllEmployees();
        }

        public Employee GetEmployeeByID(Guid employeeid)
        {
            return _employeeDL.GetEmployeeByID(employeeid);
        }

        public int InsertEmployee(Employee employee)
        {
            return _employeeDL.InsertEmployee(employee);
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

        public IEnumerable<Employee> FilterEmployees(string keyword, int limit, int offset)
        {
            return _employeeDL.FilterEmployees(keyword, limit, offset);
        }


        #endregion
    }

}
