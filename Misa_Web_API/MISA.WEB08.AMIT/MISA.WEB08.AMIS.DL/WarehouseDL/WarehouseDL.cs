﻿using Dapper;
using MISA.WEB08.AMIS.Common.Entities;
using MISA.WEB08.AMIS.Common.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    /// <summary>
    /// WarehouseDL kết nối với Database
    /// Createdby: Nguyễn Văn Cương 02/10/2022
    /// </summary>
    public class WarehouseDL: BaseDL<Warehouse>, IWarehouseDL
    {
        /// <summary>
        /// Hàm xuất danh sách kho ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Warehouse> GetWarehouseExcel()
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;

            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên stored procedure 
                string storedProcedureName = String.Format(Resource.Proc_SelectAllRecord, typeof(Warehouse).Name);

                // Thực hiện gọi vào DB để chạy procedure
                var warehouses = mysqlConnection.Query<Warehouse>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                return warehouses;
            }
        }
    }
}
