using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ORMStudy
{
    public class DBHelper
    {
        private readonly string ConnectionString = ConfigurationManager.AppSettings["connectionStrings"];

        /// <summary>
        /// 一个方法满足不同表的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel, new()
        {

            Type type = typeof(T);
            var columns = string.Join(",", type.GetProperties().Select(p => "[" + p.Name + "]"));
            var sql = string.Format("select {0} from {1}  where ID='{2}'", columns, type.Name, id);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        item.SetValue(t, reader[item.Name] is DBNull ? null : reader[item.Name]);
                    }

                    return t;
                }
                else
                {
                    return null;
                }
            }


        }
    }
}
