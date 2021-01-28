
using ORMFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMModel
{
    /// <summary>
    /// 实体与数据库表名跟字段不一致。
    /// </summary>
    [TableAttribute("Student")]
    public class tableTest : BaseModel
    {
        [Column("Name")]
        public string 名称 { get; set; }

        public int? State { get; set; }
    }
}
