using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using ORMModel;
using ORMFramework.Mapping;
using ORMFramework.DBFilter;

namespace ORMDal.SQLHepler
{
    public class DBHelper
    {
        private readonly string ConnectionString = ConfigurationManager.AppSettings["connectionStrings"];

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel, new()
        {

            Type type = typeof(T);
            var columns = string.Join(",", type.GetProperties().Select(p => p.GetMappingName()));
            var sql = string.Format("select {0} from {1}  where ID='{2}'", columns, type.GetMappingName(), id);

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
                        //获取数据库列名
                        var name = item.GetMappingName();
                        //将数据库列赋值给对应属性
                        item.SetValue(t, reader[name] is DBNull ? null : reader[name]);
                    }

                    return t;
                }
                else
                {
                    return null;
                }
            }


        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Insert<T>(T type) where T : BaseModel, new()
        {
            //获取对应类型
            var model = typeof(T);
            //获取过滤后的所有列名
            var allColumns = model.FilterKeyWithInsert();
            //构造字符串
            var columnsString = string.Join(",", allColumns.Select(p => p.GetMappingName()));
            var valueString = string.Join(",", allColumns.Select(p => "@" + p.GetMappingName()));
            var param = allColumns.Select(p => new SqlParameter("@" + p.GetMappingName(), p.GetValue(type))).ToArray();

            //拼接SQL语句
            var sql = string.Format("insert into {0} ({1}) values({2})", model.GetMappingName(), columnsString, valueString);
            //与数据库交互
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(param);
                conn.Open();
                var result = command.ExecuteNonQuery();
                return result == 1;
            }
        }
    }
}
