using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Mã lỗi trả về 
    /// createby: Nguyễn Văn Cương 20/08/2022
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi trả về
        /// </summary>
        public AMITErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Lời nhắn dành cho lập trình viên
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Lời nhắn dành cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Link thêm thông tin về lỗi
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// ID thông tin lỗi
        /// </summary>
        public string? TraceID { get; set; }

        /// <summary>
        /// contructor không thông số
        /// </summary>
        public ErrorResult()
        {

        }
        /// <summary>
        /// contructor có thông số
        /// </summary>
        public ErrorResult(AMITErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string? traceID = null)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceID = traceID;
        }
    }

}
