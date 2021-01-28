using ORMFramework.DBFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMModel
{
    public class BaseModel
    {
        /// <summary>
        /// KeyAttribute过滤ID
        /// </summary>
        [KeyAttribute]
        public int ID { get; set; }
    }
}
