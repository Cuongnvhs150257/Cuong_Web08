using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using MISA.WEB08.AMIS.Common;
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
    /// EmployeeBL kết nối với Dl
    /// Createdby: Nguyễn Văn Cương 20/09/2022
    /// </summary>

    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Contructor

        /// <summary>
        /// Contructor có thông số
        /// </summary>
        /// <param name="employeeDL"></param>

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
            
        }

        #endregion


        #region Method

        /// <summary>
        /// Hàm xuất danh sách nhân viên ra Excel
        /// Createby: Nguyễn Văn Cương 15/10/2022
        /// </summary>
        /// <returns></returns>
        public Byte[] GetEmployeeExcel()
        {
            var employees = _employeeDL.GetEmployeeExcel();

            // Khai báo Workbook
            using (var wb = new XLWorkbook())
            {
                // Khai báo số dòng bắt đầu in dữ liệu
                int currentRow = 3;

                // Khai số thứ tự
                int numberOrder = 0;

                // Tạo một sheet mới
                var ws = wb.Worksheets.Add(typeof(Employee).Name);

                // Tạo tiêu đề của sheet
                var range = ws.Range(Resource.Range_Worksheet_Employee);
                range.Merge().Value = Resource.Title_Worksheet_Employee;
                range.Style.Font.Bold = true;
                range.Style.Font.FontSize = 16;
                range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Set thuộc tính của thead
                for (int i = 1; i < 11; i++)
                {
                    ws.Cell(currentRow, i).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, i).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                    ws.Cell(currentRow, i).Style.Font.Bold = true;
                }

                // Set giá trị của head
                ws.Cell(currentRow, 1).Value = Resource.Number_Order;
                ws.Cell(currentRow, 2).Value = Resource.Employee_Code;
                ws.Cell(currentRow, 3).Value = Resource.FullName;
                ws.Cell(currentRow, 4).Value = Resource.Gender;
                ws.Cell(currentRow, 5).Value = Resource.DateOfBirth;
                ws.Cell(currentRow, 6).Value = Resource.IdentifyCode;
                ws.Cell(currentRow, 7).Value = Resource.Postions;
                ws.Cell(currentRow, 8).Value = Resource.UnitName;
                ws.Cell(currentRow, 9).Value = Resource.BankAccount;
                ws.Cell(currentRow, 10).Value = Resource.BankUnit;

                // Set giá trị của body
                foreach (var employee in employees)
                {
                    currentRow++;
                    numberOrder++;
                    ws.Cell(currentRow, 1).Value = numberOrder;
                    ws.Cell(currentRow, 2).Value = employee.EmployeeCode;
                    ws.Cell(currentRow, 3).Value = employee.FullName;
                    ws.Cell(currentRow, 4).Value = employee.Gender == Gender.Male ? "Nam" : employee.Gender == Gender.Female ? "Nữ" : employee.Gender == Gender.Other ? "Khác" : "";
                    ws.Cell(currentRow, 5).Value = employee.DateOfBirth;
                    ws.Cell(currentRow, 6).Value = employee.IdentifyCode;
                    ws.Cell(currentRow, 7).Value = employee.Postions;
                    ws.Cell(currentRow, 8).Value = employee.UnitName;
                    ws.Cell(currentRow, 9).Value = employee.BankAccount;
                    ws.Cell(currentRow, 10).Value = employee.BankUnit;

                    // Căn giữa các trường
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    ws.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    //Set độ cao của hàng
                    ws.Row(currentRow).Height = 35;

                    // Set độ rộng của cột
                    ws.Column(1).Width = 7;
                    ws.Column(2).Width = 18;
                    ws.Column(3).Width = 25;
                    ws.Column(4).Width = 17;
                    ws.Column(5).Width = 17;
                    ws.Column(6).Width = 18;
                    ws.Column(7).Width = 18;
                    ws.Column(8).Width = 21;
                    ws.Column(9).Width = 21;
                    ws.Column(10).Width = 21;

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
