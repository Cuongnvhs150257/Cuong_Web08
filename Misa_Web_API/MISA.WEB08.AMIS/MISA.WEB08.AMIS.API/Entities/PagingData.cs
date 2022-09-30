namespace MISA.WEB08.AMIS.API.Entities
{
    public class PagingData
    {

        /// <summary>
        /// Dữ liệu trả về từ API FilterEmployees và số bản ghi
        /// </summary>
        /// 

        //Danh sách nhân viên trả về
        public List<Employee> Data { get; set; }

        //Tổng số nhân viên trả về
        public int TotalCount { get; set; }
    }
}
