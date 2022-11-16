using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// Hàng hóa ứng với bảng hàng hóa
    /// create by: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class Product : BaseEntities
    {
        /// <summary>
        /// ID hàng hóa
        /// </summary>
        [PrimarKeyAttribute]
        public Guid ProductID { get; set; }

        /// <summary>
        /// mã hàng hóa
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Mã hàng hóa không được để trống")][DulicateAtrribute("Mã hàng hóa <> đã tồn tại trong hệ thống, vui lòng kiểm tra lại")]
        public string ProductCode { get; set; }

        /// <summary>
        /// tên hàng hóa
        /// </summary>
        [IsNotNullOrEmptyAtrribute("Tên hàng hóa không được để trống")]
        public string ProductName { get; set; }

        /// <summary>
        /// ID nhóm vật tư hàng hóa
        /// </summary>
        public Guid? SupplyID { get; set; }

        /// <summary>
        /// Mã nhóm vật tư hàng hóa
        /// </summary>
        public string? SupplyCode { get; set; }

        /// <summary>
        /// Tên nhóm vật tư hàng hóa
        /// </summary>
        public string? SupplyName { get; set; }

        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public Guid? UnitCalculateID { get; set; }

        /// <summary>
        /// Giá trị đơn vị tính
        /// </summary>
        public string? UnitCalculateValue { get; set; }

        /// <summary>
        /// Giảm thuế
        /// </summary>
        public int? TaxReduction { get; set; }

        /// <summary>
        /// Thời gian bảo hành
        /// </summary>
        public string? Insurance { get; set; }

        /// <summary>
        /// Số lượng tối thiểu
        /// </summary>
        public int? Amount { get; set; }

        /// <summary>
        /// Nguồn gốc
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Tính chất hàng hóa
        /// </summary>
        public int? Nature { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Describe { get; set; }

        /// <summary>
        /// Số lượng tồn
        /// </summary>
        public int? QuantityStock { get; set; }

        /// <summary>
        /// Giá trị tồn
        /// </summary>
        public int? ExistentialValue { get; set; }

        /// <summary>
        /// Diễn giải khi mua
        /// </summary>
        public string? ExplainBuy { get; set; }

        /// <summary>
        /// Diễn giải khi bán
        /// </summary>
        public string? ExplainSell { get; set; }

        /// <summary>
        /// tài khoản giảm giá
        /// </summary>
        public string? ReduceAccout { get; set; }

        /// <summary>
        /// tài khoản kho
        /// </summary>
        public string? WarehouseAccount { get; set; }

        /// <summary>
        /// tài khoản trả lại
        /// </summary>
        public string? ReturnAccount { get; set; }

        /// <summary>
        /// tài khoản doanh thu
        /// </summary>
        public string? RevenueAccount { get; set; }
        
        /// <summary>
        /// tài khoản chi phí
        /// </summary>
        public string? ExpenseAccount { get; set; }

        /// <summary>
        /// tài khoản chiết khấu
        /// </summary>
        public string? DiscountAccount { get; set; }
        
        /// <summary>
        /// tỷ lệ chiết khấu khi mua
        /// </summary>
        public Decimal? DiscountRate { get; set; }

        /// <summary>
        /// đơn giá mua cố định
        /// </summary>
        public int? FixedPrice { get; set; }

        /// <summary>
        /// đơn giá mua gần nhất
        /// </summary>
        public int? NearestPrice { get; set; }

        /// <summary>
        /// đơn giá bán
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// thuế giá trị gia tăng
        /// </summary>
        public string? VATTax { get; set; }

        /// <summary>
        /// thuế nhập khẩu
        /// </summary>
        public int? ImportTax { get; set; }

        /// <summary>
        /// thuế xuất khẩu
        /// </summary>
        public int? ExportTax { get; set; }

        /// <summary>
        /// nhóm hàng hóa chịu thuế tiêu thụ đặc biệt
        /// </summary>
        public int? SupplyExciseTax { get; set; }

        /// <summary>
        /// ID kho
        /// </summary>
        public Guid? WarehouseID { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        public string? WarehouseCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        public string? WarehouseName { get; set; }
    }
}
