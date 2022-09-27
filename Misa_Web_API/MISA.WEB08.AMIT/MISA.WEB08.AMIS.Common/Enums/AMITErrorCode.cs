namespace MISA.WEB08.AMIS.Common
{
    /// <summary>
    /// Danh sach ma loi
    /// createby: Nguyễn Văn Cương 20/08/2022
    /// </summary>
    public enum AMITErrorCode
    {
        //Lỗi Exception
        Exception = 1,

        //lỗi trùng mã

        Duplicate = 2,

        //lỗi trống mã

        EmptyCode = 3,

        //Lỗi không thêm mới thành công

        InsertError = 4,

        //Lỗi không cập nhật thành công

        UpdateError = 5,

        //Lỗi xóa không thành công

        DeleteError = 6,

        //lỗi xóa toàn bộ không thành công

        DeleteAllEror = 7
    }
}
