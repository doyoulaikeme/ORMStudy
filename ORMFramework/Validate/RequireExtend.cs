using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Validate
{
    public static class RequireExtend
    {
        /// <summary>
        /// 校验数据是否出错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ValidateData<T>(this T obj) where T : new()
        {
            var type = typeof(T);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.IsDefined(typeof(RequireAttribute), true))
                {
                    var oVal = propertyInfo.GetValue(obj);
                    if (oVal == null || string.IsNullOrWhiteSpace(oVal.ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
