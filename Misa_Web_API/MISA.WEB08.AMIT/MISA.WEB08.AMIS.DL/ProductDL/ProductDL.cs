using Dapper;
using MISA.WEB08.AMIS.Common;
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
    /// ProductDL kết nối với Database
    /// Createdby: Nguyễn Văn Cương 01/10/2022
    /// </summary>
    public class ProductDL: BaseDL<Product>, IProductDL
    {
        /// <summary>
        /// Hàm xuất danh sách hàng hóa ra Excel
        /// Createby: Nguyễn Văn Cương 16/11/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProductExcel()
        {
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;

            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                // Khai báo tên stored procedure 
                string storedProcedureName = String.Format(Resource.Proc_SelectAllRecord, typeof(Product).Name);

                // Thực hiện gọi vào DB để chạy procedure
                var products = mysqlConnection.Query<Product>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                return products;
            }
        }

        /// <summary>
        /// Hàm thêm danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDMuti"></param>
        /// <returns></returns>
        public Guid InsertMutiRecords(Product record, List<object>? RecordIDMuti)
        {
            int numberOfAffectedRows = 0;
            var newProductID = Guid.NewGuid();
            var Productproperties = typeof(Product).GetProperties();
            var parameters = new DynamicParameters();

            foreach (var property in Productproperties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                if (primaryKeyAttribue != null)
                {
                    propertyValue = newProductID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"v_{propertyName}", propertyValue);
            }
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Proc_InsertRecord, typeof(Product).Name);

                //Thực hiện gọi vào DB
                numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            if (numberOfAffectedRows > 0)
            {
                SaveGroupProduct(newProductID, RecordIDMuti);
                return newProductID;
            }
            else
            {

                return Guid.Empty;
            }

            
        }

        /// <summary>
        /// Hàm thêm Product ID, Supply ID vào bảng trung gian
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="newProductID"></param>
        /// <param name="RecordIDMuti"></param>
        /// <returns></returns>
        public int SaveGroupProduct(object? newProductID, List<object>? RecordIDMuti)
        {
            int numberOfAffectedRows = 0;
            var parameters = new DynamicParameters();

            for (int i = 0; i < RecordIDMuti.Count; i++)
            {
                var newRecordID = Guid.NewGuid();
                var properties = typeof(GroupProductSupply).GetProperties();

                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    object propertyValue;
                    var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                    if (primaryKeyAttribue != null)
                    {
                        propertyValue = newRecordID;
                    }
                    else
                    {
                        var ListIDAttribute = (ListIDAttribute?)Attribute.GetCustomAttribute(property, typeof(ListIDAttribute));
                        if (ListIDAttribute != null)
                        {
                            propertyValue = RecordIDMuti[i];
                        }
                        else
                        {
                            propertyValue = newProductID;
                        }
                    }
                    parameters.Add($"v_{propertyName}", propertyValue);
                }

                //Khởi tạo kết nối với MySQl
                string connectionString = DataContext.MySqlConnectionString;
                using (var mysqlConnection = new MySqlConnection(connectionString))
                {
                    //khai bao ten stored produre
                    string storeProdureName = String.Format(Resource.Proc_InsertRecord, typeof(GroupProductSupply).Name);

                    //Thực hiện gọi vào DB
                    numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }

            }
            if (numberOfAffectedRows > 0)
            {
                return numberOfAffectedRows;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Hàm cập nhật danh sách hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDUpdateMuti"></param>
        /// <returns></returns>
        public int UpdateMutiRecords(Product record, Guid productid, List<object>? RecordIDUpdateMuti, List<object>? GroupIDMuti)
        {
            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            var properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                if (primaryKeyAttribue != null)
                {
                    propertyValue = productid;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"v_{propertyName}", propertyValue);
            }

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Proc_UpdateRecord, typeof(Product).Name);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                DeleteGroupProduct(productid,GroupIDMuti);
                SaveGroupProduct(productid, RecordIDUpdateMuti);
                return numberOfAffectedRows;
            }
        }

        /// <summary>
        /// Hàm cập nhật bảng trung gian hàng hóa
        /// Nguyễn Văn Cương 25/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <param name="RecordIDUpdateMuti"></param>
        /// <returns></returns>
        public int DeleteGroupProduct(Guid productid, List<object>? GroupIDMuti)
        {
            int numberOfAffectedRows = 0;
            var parameters = new DynamicParameters();

            for (int i = 0; i < GroupIDMuti.Count; i++)
            {
                    var properties = typeof(GroupProductSupply).GetProperties();

                    foreach (var property in properties)
                    {
                        string propertyName = property.Name;
                        object propertyValue;
                        var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                        if (primaryKeyAttribue != null)
                        {
                            propertyValue = GroupIDMuti[i];
                            parameters.Add($"v_{propertyName}", propertyValue);
                            break;
                        }
                    }

                    //Khởi tạo kết nối với MySQl
                    string connectionString = DataContext.MySqlConnectionString;
                    using (var mysqlConnection = new MySqlConnection(connectionString))
                    {
                        //khai bao ten stored produre
                        string storeProdureName = String.Format(Resource.Proc_DeleteRecord, typeof(GroupProductSupply).Name);

                        //Thực hiện gọi vào DB
                        numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    }
            }
            if (numberOfAffectedRows > 0)
            {
                return numberOfAffectedRows;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Hàm check mã trùng
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>true, false</returns>
        public bool CheckRecordCodeExist(string recordCode)
        {
            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            var properties = typeof(Product).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var Dulicate = (DulicateAtrribute?)Attribute.GetCustomAttribute(property, typeof(DulicateAtrribute));
                if (Dulicate != null)
                {
                    parameters.Add($"v_{propertyName}", recordCode);
                    break;
                }
            }
            //khai bao ten stored produre
            var storeProdureName = String.Format(Resource.Proc_CheckDulicate, typeof(Product).Name);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;

            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                var recordCodeDuplicate = mysqlConnection.QueryFirstOrDefault<string>(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (recordCodeDuplicate != null)
                    return true;
                else
                    return false;
            };

        }

        /// <summary>
        /// Hàm cập nhật mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="number"></param>
        /// <param name="last"></param>
        public void SaveCode(string prefix, string number, string last)
        {
            var parameters = new DynamicParameters();
            parameters.Add("v_TableName", typeof(Product).Name);
            parameters.Add("v_Prefix", prefix);
            parameters.Add("v_Number", number);
            parameters.Add("v_Last", last);
            parameters.Add("v_LengthNumber", number.Length);
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = Resource.Proc_UpdateCode;

                //Thực hiện gọi vào DB
                mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);


            }
        }

        /// <summary>
        /// Hàm kết nối DB để lấy mã tự sinh
        /// Createby: Nguyễn Văn Cương 20/11/2022
        /// </summary>
        /// <returns>new code</returns>
        public object GetNewCode()
        {
            var parameters = new DynamicParameters();
            parameters.Add("v_TableName", typeof(Product).Name);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = Resource.Proc_GetNewCode;

                //Thực hiện gọi vào DB
                var newCode = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return newCode;
            };
        }

    }
}
