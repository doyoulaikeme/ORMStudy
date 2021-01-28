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

        public static string GetMappingName<T>(this T type) where T : MemberInfo
        {
            if (type.IsDefined(typeof(MappingNameAttribute), true))
            {
                var attribute = type.GetCustomAttribute<MappingNameAttribute>();
                return "[" + attribute.GetName() + "] as [" + type.Name + "]";
            }
            else
            {
                return "[" + type.Name + "]";
            }
        }


    }
}
