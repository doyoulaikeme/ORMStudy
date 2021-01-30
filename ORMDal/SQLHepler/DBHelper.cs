using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORMModel;
using ORMFramework.Mapping;
using ORMFramework.DBFilter;
using ORMFramework.Validate;

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
        public T Find<T>(Expression<Func<T, bool>> exp) where T : BaseModel, new()
        {
            var test = exp.Parameters;



            Type type = typeof(T);
            //拼接SQL语句
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Find);

            CustomVisitor<T> visitor = new CustomVisitor<T>();
            visitor.Visit(exp);
            sql += visitor.GetSqlString();
            var valDic = visitor.GetValDic();

            var parameters = valDic.Select(p => p.Value == null ? new SqlParameter(p.Key, DBNull.Value) : new SqlParameter(p.Key, p.Value)).ToArray();

            return ExecuteSql(sql, parameters, sqlcommand =>
            {
                var reader = sqlcommand.ExecuteReader();
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

            });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Insert<T>(T type) where T : BaseModel, new()
        {

            var model = typeof(T);
            //拼接SQL语句
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Insert);
            //将值参数化，并防止为Null
            var parameters = model.GetProperties().Select(p => new SqlParameter("@" + p.GetMappingName(), p.GetValue(type) ?? DBNull.Value)).ToArray();
            //与数据库交互
            return ExecuteSql<bool>(sql, parameters, SqlCommand => SqlCommand.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Update<T>(T type) where T : BaseModel, new()
        {
            if (!type.ValidateData())
            {
                throw new Exception("数据出错！");
            }

            var model = typeof(T);
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Update);
            var pts = model.GetProperties().Select(p => new SqlParameter("@" + p.GetMappingName(), p.GetValue(model) ?? DBNull.Value)).ToList();
            pts.Add(new SqlParameter("@id", type.ID));
            var parameters = pts.ToArray();
            //与数据库交互
            return ExecuteSql<bool>(sql, parameters, SqlCommand => SqlCommand.ExecuteNonQuery() > 0);

        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete<T>(Expression<Func<T, bool>> exp) where T : BaseModel, new()
        {
            //拼接SQL语句
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Delete);

            CustomVisitor<T> visitor = new CustomVisitor<T>();
            visitor.Visit(exp);
            sql += visitor.GetSqlString();
            var valDic = visitor.GetValDic();
            var parameters = valDic.Select(p => p.Value == null ? new SqlParameter(p.Key, DBNull.Value) : new SqlParameter(p.Key, p.Value)).ToArray();
            return ExecuteSql<bool>(sql, parameters, sqlcommand => sqlcommand.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 代码复用
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="func"></param>
        /// <returns></returns>

        private Q ExecuteSql<Q>(string sql, SqlParameter[] parameters, Func<SqlCommand, Q> func)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                return func.Invoke(command);
            }
        }
    }
}
