using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    /// <summary>
    /// Mã lỗi trả về 
    /// createby: Nguyễn Văn Cương 20/08/2022
    /// </summary>
    public class ErrorResult
    {
        //Mã lỗi trả về
        public AMITErrorCode ErrorCode { get; set; }

        //Lời nhắn dành cho lập trình viên
        public string DevMsg { get; set; }

        //Lời nhắn dành cho người dùng
        public string UserMsg { get; set; }

        //Link thêm thông tin về lỗi
        public string MoreInfo { get; set; }

        //ID thông tin lỗi
        public string TraceID { get; set; }
        
        //contructor không thông số
        public ErrorResult()
        {
        }
        //contructor có thông số
        public ErrorResult(AMITErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string traceID)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceID = traceID;
        }
    }

}
