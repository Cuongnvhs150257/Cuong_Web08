namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Dữ liệu trả về từ API FilterEmployees và số bản ghi
    /// Createdby: Nguyễn Văn Cương 26/09/2022
    /// </summary>
    /// 
    public class PagingData<T>
    {
        //Danh sách đối tượng trả về
        public List<T> Data { get; set; }

        //Tổng số đối tượng trả về
        public int TotalCount { get; set; }

        //Tổng số Số lượng tồn
        public Decimal? SumQuantity { get; set; }

        //Tổng số Giá trị tồn
        public Decimal? SumExistent { get; set; }
    }
}
