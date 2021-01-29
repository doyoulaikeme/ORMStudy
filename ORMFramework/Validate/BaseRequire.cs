using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Validate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class BaseRequire : Attribute
    {
        public abstract bool Validate(object obj);
    }
}
