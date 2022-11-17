using ClosedXML.Excel;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constuctor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method


        /// <summary>
        /// Hàm kết nối DL để lấy danh sách đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Hàm validate dữ liệu đầu vào
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private ServiceRespone ValidateRequestData(Boolean validateStatus, T record)
        {
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var properyValue = property.GetValue(record);
                var isNotNullOrEmptyAtrribute = (IsNotNullOrEmptyAtrribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAtrribute));
                if (isNotNullOrEmptyAtrribute != null && string.IsNullOrEmpty(properyValue?.ToString()))
                {
                    validateFailures.Add(isNotNullOrEmptyAtrribute.ErrorMessage);
                }

                var EmailAtrribute = (EmailAtrribute?)Attribute.GetCustomAttribute(property, typeof(EmailAtrribute));
                if(EmailAtrribute != null)
                {
                    var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    if(properyValue != null && !regex.IsMatch(properyValue?.ToString()))
                    {
                        validateFailures.Add(EmailAtrribute.ErrorMessage);
                    }
                    
                }
                var Dulicate = (DulicateAtrribute?)Attribute.GetCustomAttribute(property, typeof(DulicateAtrribute));
                if(Dulicate != null && validateStatus == true)
                {
                    var duli = _baseDL.CheckRecordCodeExist(properyValue.ToString());
                    if (properyValue != null && duli == true)
                    {
                        validateFailures.Add(Dulicate.ErrorMessage);
                    }
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceRespone
                {
                    Success = false,
                    Data = new ErrorResult(
                    AMITErrorCode.ValidateError,
                    Resource.DevMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFaild,
                    validateFailures[0])
                };
            }
            return new ServiceRespone
            {
                Success = true

            };
        }

        /// <summary>
        /// Hàm kết nối DB để thêm mới đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceRespone InsertRecord(T record)
        {
            var validateStatus = true;
            var validateResult = ValidateRequestData(validateStatus, record);

            if (validateResult != null && validateResult.Success)
            {
                var newRecordID = _baseDL.InsertRecords(record);

                if (newRecordID != Guid.Empty)
                {
                    return new ServiceRespone
                    {
                        Success = true,
                        Data = newRecordID
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

        /// <summary>
        /// Hàm kết nối DL để lấy nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>
        public object GetRecordByID(Guid record)
        {
            return _baseDL.GetRecordByID(record);
        }

        /// <summary>
        /// Hàm kết nối DL để phân trang, tìm kiếm
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData<T> Filter(string? where, int? limit, int? offset, int? soft, string? typesoft, string? keyword)
        {
            string query = "";
            string queryCount = "";
            if (where != null && typesoft == null)
            {
                var properties = typeof(T).GetProperties();
                string queryFirst = "";
                string queryAfter = "";
                foreach (var property in properties)
                {
                    var SearchNameAttribute = (SearchNameAttribute?)Attribute.GetCustomAttribute(property, typeof(SearchNameAttribute));
                    if (SearchNameAttribute != null)
                    {
                        string propertyName = property.Name;
                        queryFirst = propertyName + " LIKE '%" + where + "%'";
                    }
                    var SearchCodeAttribute = (SearchCodeAttribute?)Attribute.GetCustomAttribute(property, typeof(SearchCodeAttribute));
                    if (SearchCodeAttribute != null)
                    {
                        string propertyName = property.Name;
                        queryAfter = "OR " + propertyName + " LIKE '%" + where + "%'";
                    }
                    
                }
                query = queryFirst + queryAfter;

            }
            if (where != null && soft != null)
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name == typesoft)
                    {
                        string propertyName = property.Name;
                        switch (soft)
                        {
                            case 1:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " IS NULL";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " IS NULL";
                                break;
                            case 2:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " IS NOT NULL";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " IS NOT NULL";
                                break;
                            case 3:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " = " + "'" + keyword + "' ";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " = " + "'" + keyword + "' ";
                                break;
                            case 4:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " != " + "'" + keyword + "'";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " != " + "'" + keyword + "'";
                                break;
                            case 5:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " LIKE '%" + keyword + "%'";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " LIKE '%" + keyword + "%'";
                                break;
                            case 6:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " LIKE '!%" + keyword + "%'";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " LIKE '!%" + keyword + "%'";
                                break;
                            case 7:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " LIKE " + "'" + keyword + "_%'";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " LIKE " + "'" + keyword + "_%'";
                                break;
                            case 8:
                                query = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING " + propertyName + " LIKE '%_" + keyword + "'";
                                queryCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY " + propertyName + " LIKE '%_" + keyword + "'";
                                break;
                        }
                    }

                }

            }
            if (where == null && typesoft != null)
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name == typesoft)
                    {
                        string propertyName = property.Name;
                        switch (soft)
                        {
                            case 1:
                                query = propertyName + " IS NULL";
                                break;
                            case 2:
                                query = propertyName + " IS NOT NULL";
                                break;
                            case 3:
                                query = propertyName + " = " + "'" + keyword + "'";
                                break;
                            case 4:
                                query = propertyName + " != " + "'" + keyword + "'";
                                break;
                            case 5:
                                query = propertyName + " LIKE '%" + keyword + "%'";
                                break;
                            case 6:
                                query = propertyName + " LIKE '!%" + keyword + "%'";
                                break;
                            case 7:
                                query = propertyName + " LIKE " + "'" + keyword + "_%'";
                                break;
                            case 8:
                                query = propertyName + " LIKE '%_" + keyword + "'";
                                break;
                        }
                    }

                }
            }

            return _baseDL.Filter(limit, offset, query, queryCount);
        }


        /// <summary>
        /// Hàm kết nối DB để xóa đối tượng theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns>numberOfAffectedRows</returns>
        /// <summary>
        public int DeleteRecord(Guid recordid)
        {
            return _baseDL.DeleteRecord(recordid);
        }

        /// Hàm kết nối DB để sửa đối tượng theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="record"></param>
        /// <returns>numberOfAffectedRows</returns>
        public ServiceRespone UpdateRecord(Guid recordid, T record)
        {
            var validateStatus = false;
            var validateResult = ValidateRequestData(validateStatus, record);
            if (validateResult != null && validateResult.Success)
            {
                var Update = _baseDL.UpdateRecord(recordid, record);

                if (Update != null)
                {
                    return new ServiceRespone
                    {
                        Success = true,
                        Data = Update
                    };
                }
                else
                {
                    return new ServiceRespone
                    {
                        Success = false,
                        Data = new ErrorResult(
                        AMITErrorCode.UpdateError,
                        Resource.DevMsg_UpdateFailed,
                        Resource.UserMsg_UpdateFaild,
                        Resource.MoreInfo_Request)
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

        /// <summary>
        /// Hàm kết nối DB để xóa nhiều đối tượng
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteMultipleRecord(List<Guid> ListRecordID)
        {
            return _baseDL.DeleteMultipleRecord(ListRecordID);
        }

        /// <summary>
        /// Hàm kết nối DB để lấy mã đối tượng lớn nhất
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <returns></returns>
        public object GetMaxRecord()
        {
            return _baseDL.GetMaxRecord();
        }

        /// <summary>
        /// Hàm kiểm tra phát sinh trong khi xóa
        /// Nguyễn Văn Cương 15/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public object CheckDelete(Guid record)
        {
            return _baseDL.CheckDelete(record);
        }

        #endregion
    }
}
