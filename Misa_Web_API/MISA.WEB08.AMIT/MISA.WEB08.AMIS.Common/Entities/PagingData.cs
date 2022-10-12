namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Dữ liệu trả về từ API FilterEmployees và số bản ghi
    /// Createdby: Nguyễn Văn Cương 26/09/2022
    /// </summary>
    /// 
    public class PagingData
    {
        //Danh sách nhân viên trả về
        public List<Employee> Data { get; set; }

        //Tổng số nhân viên trả về
        public int TotalCount { get; set; }
    }
}
