using ORMDal.SQLHepler;
using ORMModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*************ORM研究****************");

            try
            {

                DBHelper db = new DBHelper();
                //映射
                var student = db.Find<tableTest>(1);
                var student1 = db.Find<tableTest>(2);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
