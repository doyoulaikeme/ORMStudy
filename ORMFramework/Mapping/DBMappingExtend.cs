using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Mapping
{

    public static class DBMappingExtend
    {
        /// <summary>
        /// 判断表名是否加了特性
        /// 是的话以特性表名查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTableName(this Type type)
        {
            if (type.IsDefined(typeof(TableAttribute), true))
            {
                var attribute = type.GetCustomAttribute<TableAttribute>();
                return attribute.GetTableName();
            }
            else
            {
                return type.Name;
            }
        }
    }
}
