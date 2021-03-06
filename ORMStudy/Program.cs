﻿using ORMDal.SQLHepler;
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

                //添加
                var model = new tableTest();
                model.State = 12;
                model.名称 = "测试添加";
                db.Insert(model);
                db.Insert(model);


                //修改
                var student = db.Find<tableTest>(p => p.ID == 1 || p.State == null && p.名称 == "测试");
                student.名称 += "修改";
                student.State = null;
                db.Update(student);

                //查询
                var newStudent = db.Find<tableTest>(2);

                //删除
                db.Delete<tableTest>(p => p.ID == student.ID);
                var oldStudent = db.Find<tableTest>(p => p.ID == student.ID);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
