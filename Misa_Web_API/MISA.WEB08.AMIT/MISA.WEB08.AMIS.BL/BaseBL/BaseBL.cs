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
                SaveCode(record);

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
        /// Hàm cập nhật mã tự sinh
        /// CreatedBy: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <param name="record">Bản ghi</param>
        public void SaveCode(T record)
        {
            string prefix = "";
            string number = "";
            string last = "";

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var SearchCodeAttribute = (SearchCodeAttribute?)Attribute.GetCustomAttribute(property, typeof(SearchCodeAttribute));
                if (SearchCodeAttribute != null)
                {
                    string properyValue = property.GetValue(record).ToString();
                    for (int i = 0; i < properyValue.Length; i++)
                    {
                        char[] keyNumber = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                        char temp = properyValue[i];
                        if ((keyNumber.Contains(temp)) && last == "")
                        {
                            if (number == "" && temp == '0')
                            {
                                prefix += temp;
                            }
                            else
                            {
                                number += temp;
                            }
                        }
                        else
                        {
                            if (number != "")
                            {
                                last += temp;
                            }
                            else
                            {
                                prefix += temp;
                            }
                        }
                    }
                    if (number == "")
                    {
                        number = "0";
                    }
                    _baseDL.SaveCode(prefix, number, last);
                }
            }
 
        }

        /// <summary>
        /// Hàm lấy mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <returns></returns>
        public object GetNewCode()
        {
            return _baseDL.GetNewCode();
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
        public PagingData<T> Filter(string? where, int? limit, int? offset, int? soft, string[]? typesoft, string[]? keyword)
        {
            string query = "";
            string queryFirst = "";
            string queryAfter = "";
            string queryStartCount = "";
            string queryCount = "";

            ///trường hợp search thông thường
            if (where != null && typesoft.Length == 0)
            {
                var properties = typeof(T).GetProperties();
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
            ///trường hợp cả search cả lọc
            if (where != null && soft != null)
            {
                string typename = "";
                string typequery = "";
                string key = "";
                queryFirst = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ProductID HAVING ";
                queryStartCount = "ProductName LIKE '%" + where + "%'" + "OR ProductCode LIKE '%" + where + "%' GROUP BY ";
                for (int i = 0; i < typesoft.Length; i++)
                {   
                    ///Lọc bảng đầu tiên
                    if (i == 0)
                    {
                        typequery = typesoft[i]; 
                    }
                    //Lọc bảng tiếp theo
                    else
                    {
                        typequery = "AND " + typesoft[i];
                    }
                    for (int j = i; j < keyword.Length; j++)
                    {
                        key = keyword[j];
                        break;
                    }
                    switch (soft)
                    {
                        ///Lấy tất cả
                        case 0:
                            queryAfter += typequery;
                            break;
                        /// lấy trống
                        case 1:
                            queryAfter += typequery + " IS NULL";
                            break;
                        ///lấy không trống
                        case 2:
                            queryAfter += typequery + " IS NOT NULL";
                            break;
                        ///lấy bằng
                        case 3:
                            queryAfter += typequery + " = " + "'" + key + "' ";
                            break;
                        ///lấy khác
                        case 4:
                            queryAfter += typequery + " != " + "'" + key + "'";
                            break;
                        ///lấy chứa
                        case 5:
                            queryAfter += typequery + " LIKE '%" + key + "%'";
                            break;
                        ///lấy không chứa
                        case 6:
                            queryAfter += typequery + " LIKE '!%" + key + "%'";
                            break;
                        ///lấy bắt đầu với
                        case 7:
                            queryAfter += typequery + " LIKE " + "'" + key + "_%'";
                            break;
                        ///lấy kết thúc với
                        case 8:
                            queryAfter += typequery + " LIKE '%_" + key + "'";
                            break;
                    }
                    query = queryFirst + queryAfter;
                    queryCount = queryStartCount + queryAfter;
                }

            }
            ///trường hợp chỉ lọc
           if (where == null && typesoft != null)
            {
                string typename = "";
                string typequery = "";
                string key = "";

                for (int i = 0; i < typesoft.Length; i++)
                {
                    ///Lọc bảng đầu tiên
                    if (i == 0)
                    {
                        typequery = typesoft[i];
                    }
                    ///Lọc bảng tiếp theo
                    else
                    {
                        typequery = "AND " + typesoft[i];
                    }
                    for (int j = i; j < keyword.Length; j++)
                    {
                        key = keyword[j];
                        break;
                    }
                    switch (soft)
                    {
                        ///lấy tất cả
                        case 0:
                            query += typequery;
                            break;
                        ///lấy trống
                        case 1:
                            query += typequery + " IS NULL ";
                            break;
                        ///lấy không trống
                        case 2:
                            query += typequery + " IS NOT NULL ";
                            break;
                        ///lấy bằng
                        case 3:
                            query += typequery + " = " + "'" + key + "' ";
                            break;
                        ///lấy khác
                        case 4:
                            query += typequery + " != " + "'" + key + "' ";
                            break;
                        ///lấy chứa
                        case 5:
                            query += typequery + " LIKE '%" + key + "%' ";
                            break;
                        ///lấy không chứa
                        case 6:
                            query += typequery + " LIKE '!%" + key + "%' ";
                            break;
                        ///lấy bắt đầu với
                        case 7:
                            query += typequery + " LIKE " + "'" + key + "_%' ";
                            break;
                        ///lấy kết thúc với
                        case 8:
                            query += typequery + " LIKE '%_" + key + "' ";
                            break;
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
