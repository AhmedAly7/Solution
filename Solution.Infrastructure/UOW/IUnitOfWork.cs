using System.Data.Common;
using System.Data;

namespace Solution.Infrastructure.UOW;

public interface IUnitOfWork<T>
{
	int SaveChanges();
	ValueTask<int> SaveChangesAsync();
	ValueTask<int> ExecuteQuery(string commandText, CommandType commandType = CommandType.Text, params DbParameter[] parameters);
}