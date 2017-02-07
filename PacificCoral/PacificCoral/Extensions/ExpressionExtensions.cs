using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PacificCoral.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression ReplaceParameter(this Expression expression, ParameterExpression source, Expression target)
        {
            return new ParameterReplacer { Source = source, Target = target }.Visit(expression);
        }
    }
    public class ParameterReplacer : ExpressionVisitor
    {

        public ParameterExpression Source;
        public Expression Target;


        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == Source ? Target : base.VisitParameter(node);
        }
    }
}
