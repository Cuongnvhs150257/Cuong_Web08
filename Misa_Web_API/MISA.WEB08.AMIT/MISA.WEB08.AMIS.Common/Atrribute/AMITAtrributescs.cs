namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Atrribute dùng để khai báo khóa chính
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimarKeyAttribute : Attribute
    {
        
    }

    /// <summary>
    /// Atrribute dùng để kiểm tra có null hay không
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAtrribute : Attribute
    {
        #region Filed
        /// <summary>
        /// Lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="errorMessage"></param>
        public IsNotNullOrEmptyAtrribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion



    }


}
