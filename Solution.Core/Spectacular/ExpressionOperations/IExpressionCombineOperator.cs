using System.Linq.Expressions;

namespace Solution.Core.Spectacular.ExpressionOperations;

public interface IExpressionCombineOperator
{
	Expression<Func<TModel, bool>> Combine<TModel>(Expression<Func<TModel, bool>> left, Expression<Func<TModel, bool>> right);
}
