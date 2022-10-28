using ClosedXML.Excel;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
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

        #region GetAll Records

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
                if(Dulicate != null)
                {
                    var duli = _baseDL.CheckEmployeeCodeExist(properyValue.ToString());
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
        #endregion

        #region InsertRecords

        /// <summary>
        /// Hàm kết nối DB để thêm mới đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ServiceRespone InsertRecords(T record)
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

        #endregion

        #region GetRecordByID

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

        #endregion

        #region Filter

        /// <summary>
        /// Hàm kết nối DL để phân trang, tìm kiếm
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData Filter(string keyword, int? limit, int? offset)
        {
            return _baseDL.Filter(keyword, limit, offset);
        }

        #endregion

        #region DeleteRecord

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

        #endregion

        #region UpdateRecord

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

        #endregion

        #region DeleteMultipleRecord

        /// <summary>
        /// Hàm kết nối DB để xóa nhiều đối tượng
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteMultipleRecord(List<Guid> ListEmployeeID)
        {
            return _baseDL.DeleteMultipleRecord(ListEmployeeID);
        }

        #endregion

        #region GetMaxRecord

        /// <summary>
        /// Hàm kết nối DB để lấy mã đối tượng lớn nhất
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <returns></returns>
        public object GetMaxRecord()
        {
            return _baseDL.GetMaxRecord();
        }

        #endregion

        #endregion
    }
}
