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
    /// ProductBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class ProductBL: BaseBL<Product>, IProductBL
    {
        #region Field

        private IProductDL _productDL;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor có thông số
        /// </summary>
        /// <param name="productDL"></param>

        public ProductBL(IProductDL productDL) : base(productDL)
        {
            _productDL = productDL;

        }

        #endregion
            /// <summary>
            /// Hàm xuất danh sách hàng hóa ra Excel
            /// Createby: Nguyễn Văn Cương 16/11/2022
            /// </summary>
            /// <returns></returns>
            public Byte[] GetProductExcel()
            {
                var products = _productDL.GetProductExcel();

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
                    var range = ws.Range(Resource.Range_Worksheet_Product);
                    range.Merge().Value = Resource.Title_Worksheet_Product;
                    range.Style.Font.Bold = true;
                    range.Style.Font.FontSize = 16;
                    range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // Set thuộc tính của thead
                    for (int i = 1; i < 14; i++)
                    {
                        ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Cell(currentRow, i).Style.Font.Bold = true;
                    }

                    // Set giá trị của head
                    ws.Cell(currentRow, 1).Value = Resource.Number_Order;
                    ws.Cell(currentRow, 2).Value = "Mã";
                    ws.Cell(currentRow, 3).Value = "Tên";
                    ws.Cell(currentRow, 4).Value = "Đơn vị tính";
                    ws.Cell(currentRow, 5).Value = "Giảm thuế theo NQ43";
                    ws.Cell(currentRow, 6).Value = "Tính chất";
                    ws.Cell(currentRow, 7).Value = "Nhóm vật tư hàng hóa";
                    ws.Cell(currentRow, 8).Value = "Trạng thái";
                    ws.Cell(currentRow, 9).Value = "Số lượng tồn";
                    ws.Cell(currentRow, 10).Value = "Giá trị tồn";
                    ws.Cell(currentRow, 11).Value = "Thời gian bảo hành";
                    ws.Cell(currentRow, 12).Value = "Số lượng tồn tối thiểu";
                    ws.Cell(currentRow, 13).Value = "Nguồn gốc";

                // Set giá trị của body
                foreach (var product in products)
                    {
                        currentRow++;
                        numberOrder++;
                        ws.Cell(currentRow, 1).Value = numberOrder;
                        ws.Cell(currentRow, 2).Value = product.ProductCode;
                        ws.Cell(currentRow, 3).Value = product.ProductName;
                        ws.Cell(currentRow, 4).Value = product.UnitCalculateValue;
                        ws.Cell(currentRow, 5).Value = product.TaxReduction;
                        ws.Cell(currentRow, 6).Value = product.Nature;
                        ws.Cell(currentRow, 7).Value = product.SupplyName;
                        ws.Cell(currentRow, 8).Value = product.Insurance;
                        ws.Cell(currentRow, 9).Value = product.QuantityStock;
                        ws.Cell(currentRow, 10).Value = product.ExistentialValue;
                        ws.Cell(currentRow, 11).Value = product.Insurance;
                        ws.Cell(currentRow, 12).Value = product.Amount;
                        ws.Cell(currentRow, 13).Value = product.Source;

                        // Căn giữa các trường
                        ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        ws.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                    //Set độ cao của hàng
                    ws.Row(currentRow).Height = 35;

                        // Set độ rộng của cột
                        ws.Column(1).Width = 7;
                        ws.Column(2).Width = 12;
                        ws.Column(3).Width = 18;
                        ws.Column(4).Width = 12;
                        ws.Column(5).Width = 21;
                        ws.Column(6).Width = 18;
                        ws.Column(7).Width = 21;
                        ws.Column(8).Width = 21;
                        ws.Column(9).Width = 18;
                        ws.Column(10).Width = 18;
                        ws.Column(11).Width = 18;
                        ws.Column(12).Width = 18;
                        ws.Column(13).Width = 18;

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
    }
}
