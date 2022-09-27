using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IEmployeeDL
    {
        public IEnumerable<Employee> GetAllEmployees();

        public Employee GetEmployeeByID(Guid employeeid);

        public int InsertEmployee(Employee employee);

        public int UpdateEmployee(Guid employeeid, Employee employee);

        public IEnumerable<Employee> FilterEmployees(string keyword, int limit, int offset);

        public int DeleteEmployee(Guid employeeid);

        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid);

    }
}
