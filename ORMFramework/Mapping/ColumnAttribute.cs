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
    public class ColumnAttribute : Attribute
    {
        private string _columnName = null;

        /// <summary>
        /// 初始化列名
        /// </summary>
        /// <param name="tableName"></param>
        public ColumnAttribute(string tableName)
        {
            this._columnName = tableName;
        }

        /// <summary>
        /// 获取列名
        /// </summary>
        /// <returns></returns>
        public string GetColumnName()
        {
            return this._columnName;
        }
    }
}
