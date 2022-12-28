using ClosedXML.Excel;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Enums;
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
    /// UnitCalculateBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class UnitCalculateBL: BaseBL<UnitCalculate>, IUnitCalculateBL
    {
        #region Field

        private IUnitCalculateDL _unitcalculateDL;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor có thông số
        /// </summary>
        /// <param name="supplyDL"></param>

        public UnitCalculateBL(IUnitCalculateDL unitcalculateDL) : base(unitcalculateDL)
        {
            _unitcalculateDL = unitcalculateDL;

        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách đơn vị tính ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetUnitCalculateExcel()
        {
            var unitcalculates = _unitcalculateDL.GetUnitCalculateExcel();

            // Khai báo Workbook
            using (var wb = new XLWorkbook())
            {
                // Khai báo số dòng bắt đầu in dữ liệu
                int currentRow = 3;

                // Khai số thứ tự
                int numberOrder = 0;

                // Tạo một sheet mới
                var ws = wb.Worksheets.Add(typeof(UnitCalculate).Name);

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
                ws.Cell(currentRow, 2).Value = "Đơn vị tính";
                ws.Cell(currentRow, 3).Value = "Mô tả";
                ws.Cell(currentRow, 4).Value = "Trạng thái";

                // Set giá trị của body
                foreach (var unitcalculate in unitcalculates)
                {
                    currentRow++;
                    numberOrder++;
                    ws.Cell(currentRow, 1).Value = numberOrder;
                    ws.Cell(currentRow, 2).Value = unitcalculate.UnitCalculateValue;
                    ws.Cell(currentRow, 3).Value = unitcalculate.Describe;
                    ws.Cell(currentRow, 4).Value = unitcalculate.Status == (int)Status.ACTIVE ? "Đang sử dụng" : unitcalculate.Status == (int)Status.UNACTIVE ? "Ngưng sử dụng" : unitcalculate.Status == (int)Status.UNKNOW ? "Chưa xác định" : "";

                    // Căn giữa các trường
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    //Set độ cao của hàng
                    ws.Row(currentRow).Height = 25;

                    // Set độ rộng của cột
                    ws.Column(1).Width = 7;
                    ws.Column(2).Width = 21;
                    ws.Column(3).Width = 21;
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
