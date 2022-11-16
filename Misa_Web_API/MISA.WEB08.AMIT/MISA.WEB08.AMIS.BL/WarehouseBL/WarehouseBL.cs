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
    /// WarehouseBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class WarehouseBL: BaseBL<Warehouse>, IWarehouseBL
    {
        #region Field

        private IWarehouseDL _warehouseDL;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor có thông số
        /// </summary>
        /// <param name="supplyDL"></param>

        public WarehouseBL(IWarehouseDL warehouseDL) : base(warehouseDL)
        {
            _warehouseDL = warehouseDL;

        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm xuất danh sách đơn vị tính ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetWarehouseExcel()
        {
            var warehouses = _warehouseDL.GetWarehouseExcel();

            // Khai báo Workbook
            using (var wb = new XLWorkbook())
            {
                // Khai báo số dòng bắt đầu in dữ liệu
                int currentRow = 3;

                // Khai số thứ tự
                int numberOrder = 0;

                // Tạo một sheet mới
                var ws = wb.Worksheets.Add(typeof(Warehouse).Name);

                // Tạo tiêu đề của sheet
                var range = ws.Range(Resource.Range_Worksheet_Warehouse);
                range.Merge().Value = Resource.Title_Worksheet_Warehouse;
                range.Style.Font.Bold = true;
                range.Style.Font.FontSize = 16;
                range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Set thuộc tính của thead
                for (int i = 1; i < 7; i++)
                {
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                }

                // Set giá trị của head
                ws.Cell(currentRow, 1).Value = Resource.Number_Order;
                ws.Cell(currentRow, 2).Value = "Mã kho";
                ws.Cell(currentRow, 3).Value = "Tên kho";
                ws.Cell(currentRow, 4).Value = "Địa chỉ";
                ws.Cell(currentRow, 5).Value = "Trạng thái";
                ws.Cell(currentRow, 6).Value = "Chi nhánh";

                // Set giá trị của body
                foreach (var warehouse in warehouses)
                {
                    currentRow++;
                    numberOrder++;
                    ws.Cell(currentRow, 1).Value = numberOrder;
                    ws.Cell(currentRow, 2).Value = warehouse.WarehouseCode;
                    ws.Cell(currentRow, 3).Value = warehouse.WarehouseName;
                    ws.Cell(currentRow, 4).Value = warehouse.Address;
                    ws.Cell(currentRow, 5).Value = warehouse.Status;
                    ws.Cell(currentRow, 6).Value = warehouse.Branch;

                    // Căn giữa các trường
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    //Set độ cao của hàng
                    ws.Row(currentRow).Height = 25;

                    // Set độ rộng của cột
                    ws.Column(1).Width = 7;
                    ws.Column(2).Width = 18;
                    ws.Column(3).Width = 21;
                    ws.Column(4).Width = 21;
                    ws.Column(5).Width = 18;
                    ws.Column(6).Width = 21;

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
