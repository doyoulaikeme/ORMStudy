using ORMFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMStudy
{
    [TableAttribute("Student")]
    public class tableTest : BaseModel
    {
        public string Name { get; set; }

        public int? State { get; set; }
    }
}
