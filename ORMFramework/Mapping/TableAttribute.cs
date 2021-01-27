using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Mapping
{
    public class TableAttribute : Attribute
    {
        private string _tableName = null;

        /// <summary>
        /// 初始化表名
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName)
        {
            this._tableName = tableName;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return this._tableName;
        }
    }
}
