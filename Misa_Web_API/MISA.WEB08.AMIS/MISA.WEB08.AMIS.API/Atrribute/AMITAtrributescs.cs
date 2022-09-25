namespace MISA.WEB08.AMIS.API.Atrribute
{
    /// <summary>
    /// Atrribute dùng để khai báo khóa chính
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimarKeyAttribute : Attribute
    {
        
    }


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
