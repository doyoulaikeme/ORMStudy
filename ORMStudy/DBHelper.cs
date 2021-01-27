using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMStudy
{
    public class DBHelper
    {
        public T Find<T>(int id) where T : BaseModel
        {

            Type type = typeof(T);

            var sql = "";

          
        }
    }
}
