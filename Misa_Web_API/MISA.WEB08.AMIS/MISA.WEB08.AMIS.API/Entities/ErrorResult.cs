using MISA.WEB08.AMIS.API.Enums;

namespace MISA.WEB08.AMIS.API.Entities
{
    public class ErrorResult
    {
        public AMITErrorCode ErrorCode { get; set; }

        public string DevMsg { get; set; }

        public string UserMsg { get; set; }

        public string MoreInfo { get; set; }

        public string TraceID { get; set; }

        public ErrorResult()
        {
        }

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
