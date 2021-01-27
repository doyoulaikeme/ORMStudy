using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMStudy
{
    public class Student : BaseModel
    {
        public string Name { get; set; }

        public int? State { get; set; }
    }
}
