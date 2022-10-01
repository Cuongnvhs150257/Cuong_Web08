using Dapper;
using MISA.WEB08.AMIS.Common;
using MISA.WEB08.AMIS.Common.Entities;
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

        /// <summary>
        /// Hàm kết nối DB để lấy nhân viên theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns>numberOfAffectedRows</returns>
        public object GetRecordByID(Guid record)
        {
            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                parameters.Add($"v_{propertyName}", record);
                break;
            }

            //khởi tạo kết nối Database
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Pro_SelectEmployee, typeof(T).Name);

                //Thực hiện gọi vào DB
                var OJ = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return OJ;
            }
        }

        /// <summary>
        /// Hàm kết nối DB để sửa đối tượng theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="record"></param>
        /// <returns>numberOfAffectedRows</returns>
        public int UpdateRecord(Guid recordid, T record)
        {

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                object propertyValue;
                var primaryKeyAttribue = (PrimarKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimarKeyAttribute));
                if (primaryKeyAttribue != null)
                {
                    propertyValue = recordid;
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
                string storeProdureName = String.Format(Resource.Pro_UpdateEmployee, typeof(Employee).Name);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return numberOfAffectedRows;
            }    
        }

        /// <summary>
        /// Hàm kết nối DB để phân trang, tìm kiếm
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData Filter(
            string where,
            int? limit,
            int? offset)
        {
            var result = new PagingData();

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();

            parameters.Add("v_limit", limit);
            parameters.Add("v_offset", offset);
            parameters.Add("v_where", where);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Pro_Filter, typeof(T).Name);

                //Thực hiện gọi vào DB
                var ListOject = mysqlConnection.QueryMultiple(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = ListOject.Read<Employee>().ToList();
                result.TotalCount = ListOject.Read<Int32>().First();
            }

                

            return new PagingData()
            {
                Data = result.Data,
                TotalCount = result.TotalCount,
            };
        }

        /// <summary>
        /// Hàm kết nối DB để xóa đối tượng theo ID
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="recordid"></param>
        /// <returns>numberOfAffectedRows</returns>
        public int DeleteRecord(Guid recordid)
        {
            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                parameters.Add($"v_{propertyName}", recordid);
                break;
            }

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Pro_DeleteEmployee, typeof(Employee).Name);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.Execute(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfAffectedRows;
            }
    
        }

        /// <summary>
        /// Hàm kết nối DB để xóa nhiều đối tượng
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<T> DeleteMultipleRecord(List<string> employeeid)
        {
            throw new NotImplementedException();
        }
    }
}
