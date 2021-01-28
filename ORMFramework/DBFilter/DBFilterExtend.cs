using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ORMFramework.DBFilter
{
    public static class DBFilterExtend
    {
        /// <summary>
        /// 剔除多余属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> FilterKeyWithInsert(this Type type)
        {
            //过滤掉标有KeyAttribute特性的属性
            return type.GetProperties().Where(p => !p.IsDefined(typeof(KeyAttribute), true));
        }
    }
}
