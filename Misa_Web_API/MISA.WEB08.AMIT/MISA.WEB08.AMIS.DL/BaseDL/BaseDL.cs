using Dapper;
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
    }
}
