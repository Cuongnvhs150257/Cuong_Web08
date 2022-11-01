using MISA.WEB08.AMIS.Common;

namespace MISA.WEB08.AMIS.Common.Entities
{
    public class Product : BaseEntities
    {
        [PrimarKeyAttribute]
        public Guid ProductID { get; set; }

        [IsNotNullOrEmptyAtrribute("Mã hàng hóa không được để trống")][DulicateAtrribute("Mã hàng hóa <> đã tồn tại trong hệ thống, vui lòng kiểm tra lại")]
        public string ProductCode { get; set; }

        [IsNotNullOrEmptyAtrribute("Tên nhân viên không được để trống")]
        public string ProductName { get; set; }

        public Guid SupplyID { get; set; }

        public string SupplyCode { get; set; }

        public string SupplyName { get; set; }

        public Guid UnitCalculateID { get; set; }

        public string UnitCalculateValue { get; set; }

        public int TaxReduction { get; set; }

        public string Insurance { get; set; }

        public int Amout { get; set; }

        public string Source { get; set; }

        public string Nature { get; set; }

        public string Describe { get; set; }

        public int QuantityStock { get; set; }

        public int ExistentialValue { get; set; }

        public string ExplainBuy { get; set; }

        public string ExplainSell { get; set; }

        public string ReduceAccout { get; set; }

        public string WarehouseAccount { get; set; }

        public string ReturnAccount { get; set; }

        public string RevenueAccount { get; set; }

        public string ExpenseAccount { get; set; }

        public string DiscountAccount { get; set; }

        public Decimal DiscountRate { get; set; }

        public int FixedPrice { get; set; }

        public int NearestPrice { get; set; }

        public int Price { get; set; }

        public string VATTax { get; set; }

        public int ImportTax { get; set; }

        public int ExportTax { get; set; }

        public int SupplyExciseTax { get; set; }

        public Guid WarehouseID { get; set; }

        public string WarehouseCode { get; set; }

        public string WarehouseName { get; set; }
    }
}
