namespace MISA.WEB08.AMIS.API.Enums
{
    /// <summary>
    /// Danh sach ma loi
    /// </summary>
    public enum AMITErrorCode
    {
        //Lỗi Exception

        Exception = 1,

        //lỗi trùng mã

        Duplicate = 2,

        //lỗi trống mã

        EmptyCode = 3,

        //Khong them moi duoc

        InsertError = 4,

        UpdateError = 5,

        DeleteError = 6,

        DeleteAllEror = 7
    }
}
