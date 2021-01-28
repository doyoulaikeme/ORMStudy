﻿using System;
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
            //拼接SQL语句
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Find);
            var parameters = new SqlParameter[] {
            new SqlParameter("@id",id)
            };
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
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

            var model = typeof(T);
            //拼接SQL语句
            var sql = SqlCacheHelper<T>.GetSql(SqlCacheBuilderType.Insert);
            //将值参数化，并防止为Null
            var param = model.GetProperties().Select(p => new SqlParameter("@" + p.GetMappingName(), p.GetValue(type) ?? DBNull.Value)).ToArray();
            //与数据库交互
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(param);
                conn.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
