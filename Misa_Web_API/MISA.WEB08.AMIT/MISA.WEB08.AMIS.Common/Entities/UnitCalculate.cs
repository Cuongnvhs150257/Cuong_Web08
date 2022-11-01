

namespace MISA.WEB08.AMIS.Common.Entities
{
    public class UnitCalculate: BaseEntities
    {
        [PrimarKeyAttribute]
        public Guid UnitCalculateID { get; set; }

        [IsNotNullOrEmptyAtrribute("Tên đơn vị tính không được để trống")]
        public string UnitCalculateValue { get; set; }

        public string Describe { get; set; }

        public int Status { get; set; }
    }
}
