using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMFramework.Mapping
{

    public class MappingNameAttribute : Attribute
    {
        private string _Name = null;

        /// <summary>
        /// 初始化列名
        /// </summary>
        /// <param name="tableName"></param>
        public MappingNameAttribute(string _newName)
        {
            this._Name = _newName;
        }

        /// <summary>
        /// 获取列名
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this._Name;
        }
    }
}
