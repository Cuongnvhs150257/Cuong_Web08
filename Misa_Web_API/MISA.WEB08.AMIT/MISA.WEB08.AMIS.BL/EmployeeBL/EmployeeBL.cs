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

        private ServiceRespone ValidateRequestData(Employee employee)
        {
            var properties = typeof(Employee).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var properyValue = property.GetValue(employee);
                var isNotNullOrEmptyAtrribute = (IsNotNullOrEmptyAtrribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAtrribute));
                if (isNotNullOrEmptyAtrribute != null && string.IsNullOrEmpty(properyValue?.ToString()))
                {
                    validateFailures.Add(isNotNullOrEmptyAtrribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceRespone
                {
                    Success = false,
                    Data = new ErrorResult(
                    AMITErrorCode.InsertError,
                    Resource.DevMsg_InsertFailed,
                    Resource.UserMsg_InsertFaild,
                    Resource.MoreInfo_InsertFaild)
            };
            }
            return new ServiceRespone
            {
                Success = true

            };
        }

        public ServiceRespone InsertEmployee(Employee employee)
        {
            var validateResult = ValidateRequestData(employee);

            if(validateResult != null && validateResult.Success)
            {
               var newEmployeeID = _employeeDL.InsertEmployee(employee);

                if(newEmployeeID != Guid.Empty)
                {
                    return new ServiceRespone
                    {
                        Success = true,
                        Data = newEmployeeID
                    };
                }
                else
                {
                    return new ServiceRespone
                    {
                        Success = false,
                        Data = new ErrorResult(
                        AMITErrorCode.InsertError,
                        Resource.DevMsg_InsertFailed,
                        Resource.UserMsg_InsertFaild,
                        Resource.MoreInfo_InsertFaild)
                    };
                }
            }
            else
            {
                return new ServiceRespone
                {
                    Success = false,
                    Data = validateResult.Data
                };
            }

            
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
