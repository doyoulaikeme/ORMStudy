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
    /// 所以相同类型的添加会调用同一个类，不会再重新生成相同的类。
    /// 
    /// 例如 
    /// 添加 class1 然后添加class2 再添加class1。
    /// 内部运行路径是：class1 _insertCacheSql字符串-》class1 SqlCacheHelper生成
    /// =》class2 _insertCacheSql字符串-》class2 SqlCacheHelper生成
    /// =》直接获取class1 _insertCacheSql字符串。
    /// 
    /// 因为字符串是不可变的，静态会一直存储在程序结束，引用地址一直存在。
    /// 
    /// </summary>
    public class SqlCacheHelper<T> where T : BaseModel, new()
    {
        /// <summary>
        /// 优点：读取的性能快。
        /// 缺点：只能跟类型相关，一个类型只能存一份，不适合更新。
        /// </summary>
        private static string _insertCacheSql = null;
        private static string _findCacheSql = null;
        private static string _deleteCacheSql = null;
        private static string _updateCacheSql = null;


        /// <summary>
        /// 静态初始化
        /// </summary>
        static SqlCacheHelper()
        {

            {
                Type type = typeof(T);
                var columns = string.Join(",", type.GetProperties().Select(p => p.GetMappingName()));
                _findCacheSql = string.Format("select {0} from {1}   ", columns, type.GetMappingName());
                _deleteCacheSql = string.Format("delete  from {0}  ", type.GetMappingName());
            }

            {
                //获取对应类型
                var model = typeof(T);
                //获取过滤后的所有列名
                var allColumns = model.FilterKeyWithSql();
                //构造字符串
                var columnsString = string.Join(",", allColumns.Select(p => p.GetMappingName()));
                var valueString = string.Join(",", allColumns.Select(p => "@" + p.GetMappingName()));
                //拼接SQL语句
                _insertCacheSql = string.Format("insert into {0} ({1}) values({2})", model.GetMappingName(), columnsString, valueString);

            }

            {
                var model = typeof(T);
                var filterColumns = model.FilterKeyWithSql();
                var columnsString = string.Join(",", filterColumns.Select(p => p.GetMappingName() + "=@" + p.GetMappingName()));
                _updateCacheSql = string.Format("update [{0}] set {1} where id=@id", model.GetMappingName(), columnsString);
            }
        }

        /// <summary>
        /// 获取缓存语句
        /// </summary>
        /// <returns></returns>
        public static string GetSql(SqlCacheBuilderType type)
        {
            switch (type)
            {
                case SqlCacheBuilderType.Find:
                    return _findCacheSql;
                case SqlCacheBuilderType.Insert:
                    return _insertCacheSql;
                case SqlCacheBuilderType.Delete:
                    return _deleteCacheSql;
                case SqlCacheBuilderType.Update:
                    return _updateCacheSql;
                default:
                    throw new Exception("SqlCacheBuilderType Not Found！");
            }
        }

    }

    public enum SqlCacheBuilderType
    {
        Find,
        Insert,
        Update,
        Delete
    }

    /// <summary>
    ///字典缓存
    ///优势：存储方便灵活，数据以key为准
    ///劣势：数据1W以上性能开始下降
    ///hash存储-增删改查的都差不多，数据过多会有损耗。
    /// </summary>
    internal class customCache
    {
        private static Dictionary<string, string> _customCacheSql = new Dictionary<string, string>();

        public static void Add(string key, string value)
        {
            _customCacheSql.Add(key, value);
        }

        public static string GetSql(string key)
        {
            return _customCacheSql[key];
        }

    }
}
