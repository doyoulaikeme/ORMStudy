using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Mapping
{
    /// <summary>
    /// 只允许类使用该特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : MappingNameAttribute
    {
 
        /// <summary>
        /// 初始化表名
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName) : base(tableName)
        {

        }


    }
}
