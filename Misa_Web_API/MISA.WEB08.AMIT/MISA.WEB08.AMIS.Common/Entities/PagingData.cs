namespace MISA.WEB08.AMIS.Common.Entities
{
    public class PagingData
    {

        /// <summary>
        /// Dữ liệu trả về từ API FilterEmployees và số bản ghi
        /// Createdby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// 

        //Danh sách nhân viên trả về
        public List<Employee> Data { get; set; }

        //Tổng số nhân viên trả về
        public int TotalCount { get; set; }
    }
}
