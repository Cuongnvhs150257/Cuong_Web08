using Dapper;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Resource;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        // <summary>
        /// Hàm kết nối DB để lấy toàn bộ nhân viên
        /// Createdby: Nguyễn Văn Cương 28/09/2022
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllRecords()
        {

            //khai bao ten stored produre
            string storeProdureName = String.Format(Resource.Pro_SeleteAll, typeof(T).Name);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                var employees = mysqlConnection.Query<T>(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);
                return employees;
            };
        }

        /// <summary>
        /// Hàm kết nối DB để thêm mới đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Guid InsertRecords(T record)
        {

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            var newRecordID = Guid.NewGuid();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                if(primaryKeyAttribue != null)
                {
                    propertyValue = newRecordID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"v_{propertyName}", propertyValue);
            }
            
            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            int numberOfAffectedRows = 0;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Pro_InsertEmployee, typeof(T).Name);

                //Thực hiện gọi vào DB
                numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);        
            }
            if (numberOfAffectedRows > 0)
            {
                return newRecordID;
            }
            else
            {

                return Guid.Empty;
            }

        }
    }
}
