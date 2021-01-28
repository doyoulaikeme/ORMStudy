using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Mapping
{
    /// <summary>
    /// 只允许属性使用改特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : MappingNameAttribute
    {

        /// <summary>
        /// 初始化列名
        /// </summary>
        /// <param name="tableName"></param>
        public ColumnAttribute(string tableName) : base(tableName)
        {
           
        }

     
    }
}
