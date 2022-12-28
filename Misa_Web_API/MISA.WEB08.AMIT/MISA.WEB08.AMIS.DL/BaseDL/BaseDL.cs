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
    public class BaseDL<T> : IBaseDL<T>
    {
        // <summary>
        /// Hàm kết nối DB để lấy toàn bộ nhân viên
        /// Createdby: Nguyễn Văn Cương 28/09/2022
        /// </summary>
        /// <returns>all record</returns>
        public IEnumerable<T> GetAllRecords()
        {

            //khai bao ten stored produre
            string storeProdureName = String.Format(Resource.Proc_SelectAllRecord, typeof(T).Name);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                var records = mysqlConnection.Query<T>(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);
                return records;
            };
        }

        /// <summary>
        /// Hàm kết nối DB để thêm mới đối tượng
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns>new recordid</returns>
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
                string storeProdureName = String.Format(Resource.Proc_InsertRecord, typeof(T).Name);

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
                string storeProdureName = String.Format(Resource.Proc_SelectRecord, typeof(T).Name);

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
                string storeProdureName = String.Format(Resource.Proc_UpdateRecord, typeof(T).Name);

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
        /// <param name="soft"></param>
        /// <returns>Data, TotalCount</returns>
        public PagingData<T> Filter(
            int? limit,
            int? offset,
            string? query,
            Boolean? status)
        {
            var result = new PagingData<T>();

            //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
            var parameters = new DynamicParameters();

            parameters.Add("v_limit", limit);
            parameters.Add("v_offset", offset);
            parameters.Add("v_query", query);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //khai bao ten stored produre
                string storeProdureName = String.Format(Resource.Proc_Filter, typeof(T).Name);

                //Thực hiện gọi vào DB
                var ListOject = mysqlConnection.QueryMultiple(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                result.Data = ListOject.Read<T>().ToList();
                result.TotalCount = ListOject.Read<Int32>().First();
                if(status == true)
                {
                    result.SumQuantity = ListOject.Read<Decimal>().First();
                    result.SumExistent = ListOject.Read<Decimal>().First();
                }
            }
            return new PagingData<T>
            {
                Data = result.Data,
                TotalCount = result.TotalCount,
                SumQuantity = result.SumQuantity,
                SumExistent = result.SumExistent,
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
                string storeProdureName = String.Format(Resource.Proc_DeleteRecord, typeof(T).Name);

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
        public int DeleteMultipleRecord(List<Guid> ListRecordID)
        {
            try
            {
                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();
                var ListID = "";

                if (ListRecordID == null || ListRecordID.Count == 0)
                {
                    return 0;
                }

                ListID = $"('{String.Join("','", ListRecordID)}')";
                parameters.Add($"v_ListRecordID", ListID);


                //Khởi tạo kết nối với MySQl
                string connectionString = DataContext.MySqlConnectionString;
                using (var mysqlConnection = new MySqlConnection(connectionString))
                {
                    mysqlConnection.Open();
                    using (var transaction = mysqlConnection.BeginTransaction())
                    {
                        try
                        {
                            //khai bao ten stored produre
                            string storeProdureName = String.Format(Resource.Proc_DeleteMultiple, typeof(T).Name);

                            //Thực hiện gọi vào DB
                            var result = mysqlConnection.Execute(storeProdureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                            transaction.Commit();
                            mysqlConnection.Close();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        /// <summary>
        /// Hàm kết nối DB để lấy mã đối tượng lớn nhất
        /// Createby: Nguyễn Văn Cương 26/09/2022
        /// </summary>
        /// <returns>max code</returns>
        public object GetMaxRecord()
        {
            //khai bao ten stored produre
            string storeProdureName = String.Format(Resource.Proc_GetMaxRecord, typeof(T).Name);

            //Khởi tạo kết nối với MySQl
            string connectionString = DataContext.MySqlConnectionString;
            using (var mysqlConnection = new MySqlConnection(connectionString))
            {
                //Thực hiện gọi vào DB
                var recordCode = mysqlConnection.QueryFirstOrDefault(storeProdureName, commandType: System.Data.CommandType.StoredProcedure);
                return recordCode;
            };
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

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var Dulicate = (DulicateAtrribute?)Attribute.GetCustomAttribute(property, typeof(DulicateAtrribute));
                if(Dulicate != null)
                {
                    parameters.Add($"v_{propertyName}", recordCode);
                    break;
                }
            }
            //khai bao ten stored produre
            var storeProdureName = String.Format(Resource.Proc_CheckDulicate, typeof(T).Name);

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
        /// Hàm kiểm tra phát sinh trong khi xóa
        /// Nguyễn Văn Cương 15/11/2022
        /// </summary>
        /// <param name="record"></param>
        /// <returns>true, false</returns>
        public object CheckDelete(Guid record)
        {
            //Chuẩn bị tham số đầu vào
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
                string storeProdureName = String.Format(Resource.Proc_CheckDelete, typeof(T).Name);

                //Thực hiện gọi vào DB
                var OJ = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return OJ;
            }
        }
    }
}
