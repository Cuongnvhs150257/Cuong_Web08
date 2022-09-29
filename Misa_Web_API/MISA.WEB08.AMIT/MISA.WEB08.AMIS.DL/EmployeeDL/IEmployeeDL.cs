﻿using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {

        public Employee GetEmployeeByID(Guid employeeid);

        public Guid InsertEmployee(Employee employee);

        public int UpdateEmployee(Guid employeeid, Employee employee);

        public PagingData FilterEmployees(string where, int? limit, int? offset);

        public int DeleteEmployee(Guid employeeid);

        public IEnumerable<Employee> DeleteMultipleEmployee(List<string> employeeid);

    }
}