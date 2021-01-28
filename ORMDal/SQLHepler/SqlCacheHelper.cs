using ORMFramework.DBFilter;
using ORMFramework.Mapping;
using ORMModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMDal.SQLHepler
{
    /// <summary>
    /// 泛型缓存，负责生成SQL语句。
    /// T类型不同时，_insertCacheSql，SqlCacheHelper()会重新生成。
    /// 所以相同类型的添加会调用同一个类。
    /// </summary>
    public class SqlCacheHelper<T> where T : BaseModel, new()
    {
        private static string _insertCacheSql = null;
        /// <summary>
        /// 静态初始化
        /// </summary>
        static SqlCacheHelper()
        {
            //获取对应类型
            var model = typeof(T);
            //获取过滤后的所有列名
            var allColumns = model.FilterKeyWithInsert();
            //构造字符串
            var columnsString = string.Join(",", allColumns.Select(p => p.GetMappingName()));
            var valueString = string.Join(",", allColumns.Select(p => "@" + p.GetMappingName()));
            //拼接SQL语句
            _insertCacheSql = string.Format("insert into {0} ({1}) values({2})", model.GetMappingName(), columnsString, valueString);
        }

        public static string GetSql()
        {
            return _insertCacheSql;
        }
    }
}
