using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Validate
{
    /// <summary>
    /// 校验长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : BaseRequire
    {
        public int _min = 0;
        public int _max = 0;
        public LengthAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override bool Validate(object obj)
        {
            return obj != null && !string.IsNullOrWhiteSpace(obj.ToString()) && (obj.ToString().Length >= _min && obj.ToString().Length <= _max);
        }
    }
}
