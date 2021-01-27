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

            DBHelper db = new DBHelper();
            var student = db.Find<Student>(1);
            var student1 = db.Find<Student>(2);

            Console.ReadKey();
        }
    }
}
