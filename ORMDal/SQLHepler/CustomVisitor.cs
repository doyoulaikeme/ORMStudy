using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORMFramework.Mapping;

namespace ORMDal.SQLHepler
{
    /// <summary>
    /// 递归表达式树目录访问
    /// </summary>
    public class CustomVisitor<T> : ExpressionVisitor
    {
        private int _i = 0;
        private Stack<string> sqlStr = new Stack<string>();
        private Dictionary<string, string> valStr = new Dictionary<string, string>();


        internal string GetSqlString()
        {
            return " where " + string.Join(" ", sqlStr);
        }

        internal Dictionary<string, string> GetValDic()
        {
            return valStr;
        }



        protected override Expression VisitBinary(BinaryExpression node)
        {

            if (node == null) throw new ArgumentNullException("BinaryExpression");
            sqlStr.Push(")");
            Visit(node.Right);
            sqlStr.Push(" " + node.NodeType.ToSqlOperator() + " ");
            Visit(node.Left);
            sqlStr.Push("(");
            return node;

        }

        /// <summary>
        /// 常量表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node == null) throw new ArgumentNullException("ConstantExpression");

            sqlStr.Push("@" + _i);
            valStr.Add("@" + _i, node.Value?.ToString());
            _i++;
            return node;
        }


        /// <summary>
        /// 字段表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node == null) throw new ArgumentNullException("MemberExpression");
            Type type = typeof(T);
            var nowName = type.GetProperties().FirstOrDefault(p => p.Name == node.Member.Name);
            if (nowName == null)
            {
                throw new ArgumentNullException(node.Member.Name + "字段不存在！");
            }

            sqlStr.Push("[" + nowName.GetMappingName() + "]");
            return base.VisitMember(node);
        }

        /// <summary>
        /// 方法表达式
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node == null) throw new ArgumentNullException("MethodCallExpression");

            string format;
            switch (node.Method.Name)
            {
                case "StartsWith":
                    format = "({0}) LIKE {1}+'%'";
                    break;
                case "Contains":
                    format = "({0}) LIKE '%'+{1}+'%'";
                    break;
                case "EndsWith":
                    format = "({0}) LIKE '%'+{1}";
                    break;
                default:
                    throw new NotSupportedException(node.NodeType + " is not supported！");

            }
            this.Visit(node.Object);
            this.Visit(node.Arguments[0]);
            var right = sqlStr.Pop();
            var left = sqlStr.Pop();
            sqlStr.Push(string.Format(format, left, right));
            return node;
        }
    }
}
