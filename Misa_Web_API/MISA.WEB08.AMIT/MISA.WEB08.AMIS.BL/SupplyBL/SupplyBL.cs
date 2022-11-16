using ClosedXML.Excel;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MISA.WEB08.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.BL
{
    /// <summary>
    /// SupplyBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class SupplyBL: BaseBL<Supply>, ISupplyBL
    {
        #region Field

        private ISupplyDL _supplyDL;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor có thông số
        /// </summary>
        /// <param name="supplyDL"></param>

        public SupplyBL(ISupplyDL supplyDL) : base(supplyDL)
        {
            _supplyDL = supplyDL;

        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách nhóm vật tư hàng hóa ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetSupplyExcel()
        {
            var supplys = _supplyDL.GetSupplyExcel();

            // Khai báo Workbook
            using (var wb = new XLWorkbook())
            {
                // Khai báo số dòng bắt đầu in dữ liệu
                int currentRow = 3;

                // Khai số thứ tự
                int numberOrder = 0;

                // Tạo một sheet mới
                var ws = wb.Worksheets.Add(typeof(Product).Name);

                // Tạo tiêu đề của sheet
                var range = ws.Range(Resource.Range_Worksheet_Supply);
                range.Merge().Value = Resource.Title_Worksheet_Supply;
                range.Style.Font.Bold = true;
                range.Style.Font.FontSize = 16;
                range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Set thuộc tính của thead
                for (int i = 1; i < 5; i++)
                {
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                }

                // Set giá trị của head
                ws.Cell(currentRow, 1).Value = Resource.Number_Order;
                ws.Cell(currentRow, 2).Value = "Mã nhóm vật tư, hàng hóa, dịch vụ";
                ws.Cell(currentRow, 3).Value = "Tên nhóm vật tư, hàng hóa, dịch vụ";
                ws.Cell(currentRow, 4).Value = "Trạng thái";

                // Set giá trị của body
                foreach (var supply in supplys)
                {
                    currentRow++;
                    numberOrder++;
                    ws.Cell(currentRow, 1).Value = numberOrder;
                    ws.Cell(currentRow, 2).Value = supply.SupplyCode;
                    ws.Cell(currentRow, 3).Value = supply.SupplyName;
                    ws.Cell(currentRow, 4).Value = supply.Status;

                    // Căn giữa các trường
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    //Set độ cao của hàng
                    ws.Row(currentRow).Height = 25;

                    // Set độ rộng của cột
                    ws.Column(1).Width = 7;
                    ws.Column(2).Width = 33;
                    ws.Column(3).Width = 33;
                    ws.Column(4).Width = 18;

                }

                // Khai báo memoryStream để dữ liệu lưu trữ trên bộ nhớ
                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }

        }

        #endregion

    }
}
