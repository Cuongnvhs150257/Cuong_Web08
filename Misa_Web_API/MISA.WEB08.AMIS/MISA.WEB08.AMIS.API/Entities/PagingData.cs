namespace MISA.WEB08.AMIS.API.Entities
{
    public class PagingData
    {

        /// <summary>
        /// Dữ liệu trả về từ API FilterEmployees và số bản ghi
        /// </summary>
        public List<Employee> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
