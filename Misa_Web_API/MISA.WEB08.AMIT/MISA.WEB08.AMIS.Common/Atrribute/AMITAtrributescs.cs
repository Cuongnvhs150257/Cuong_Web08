namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Atrribute dùng để khai báo khóa chính
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimarKeyAttribute : Attribute
    {
        
    }

    /// <summary>
    /// Atrribute dùng để khai báo kiểm tra dulicate
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DulicateAtrribute : Attribute
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
        /// <param name="regex"></param>
        public DulicateAtrribute(string errorMess)
        {
            ErrorMessage = errorMess;
        }

        #endregion
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
    /// <summary>
    /// Atrribute dùng để kiểm tra có null hay không
    /// Createdby: Nguyễn Văn Cương 25/09/2022
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAtrribute : Attribute
    {
        #region Filed
        /// <summary>
        /// Lỗi trả về cho client
        /// </summary>
        public string Regex;
        public string ErrorMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="regex"></param>
        public EmailAtrribute(string regex, string errorMess)
        {
            Regex = regex;
            ErrorMessage = errorMess;
        }
    
        #endregion



    }

    /// <summary>
    /// Atrribute dùng để tìm kiếm tên
    /// Createdby: Nguyễn Văn Cương 17/11/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchNameAttribute : Attribute
    {

    }

    /// <summary>
    /// Atrribute dùng để tìm kiếm mã
    /// Createdby: Nguyễn Văn Cương 17/11/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SearchCodeAttribute : Attribute
    {

    }


    /// <summary>
    /// Atrribute dùng để lấy danh sách
    /// Createdby: Nguyễn Văn Cương 17/11/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ListIDAttribute : Attribute
    {

    }

    /// <summary>
    /// Atrribute dùng để lấy ProductID
    /// Createdby: Nguyễn Văn Cương 17/11/2022
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ProductAttribute : Attribute
    {

    }

}
