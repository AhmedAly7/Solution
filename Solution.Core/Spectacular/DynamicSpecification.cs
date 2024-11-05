using System.Linq.Expressions;

namespace Solution.Core.Spectacular;

public class DynamicSpecification<TModel> : AbstractSpecification<TModel>
{
	public DynamicSpecification(Expression<Func<TModel, bool>> expression) : base(expression)
	{
	}
}
