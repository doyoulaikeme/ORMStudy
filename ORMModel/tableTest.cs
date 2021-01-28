
using ORMFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMModel
{
    /// <summary>
    /// 例如数据库表名为Student,类名为testTable,将转化为[Student] as [testTable]
    /// </summary>
    [TableAttribute("Student")]
    public class tableTest : BaseModel
    {
        /// <summary>
        /// 例如数据库列名为Name,类名为名称,将转化为[Name] as [名称]
        /// </summary>
        [Column("Name")]
        public string 名称 { get; set; }

        public int? State { get; set; }
    }
}
