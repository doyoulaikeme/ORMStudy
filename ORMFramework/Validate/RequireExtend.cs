using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                //通过抽象方法
                if (propertyInfo.IsDefined(typeof(BaseRequire), true))
                {
                    var oVal = propertyInfo.GetValue(obj);
                    var attributeList = propertyInfo.GetCustomAttributes<BaseRequire>();
                    foreach (var attribute in attributeList)
                    {
                        if (!attribute.Validate(oVal))
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
        }
    }
}
