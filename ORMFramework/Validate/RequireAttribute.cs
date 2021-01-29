using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Validate
{
    /// <summary>
    /// 校验非空
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequireAttribute : BaseRequire
    {
        public override bool Validate(object obj)
        {
            return obj != null && !string.IsNullOrWhiteSpace(obj.ToString());
        }

    }
}
