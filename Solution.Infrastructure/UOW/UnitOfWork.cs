using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Solution.Data.DbContexts;

namespace Solution.Infrastructure.UOW;

public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
{
	private readonly DatabaseContext _dbContext;
	private bool disposed = false;

	public UnitOfWork(DatabaseContext dbContext)
	{
		_dbContext = dbContext;
	}

	public int SaveChanges()
	{
		return _dbContext.SaveChanges();
	}

	public async ValueTask<int> SaveChangesAsync()
	{
		return await _dbContext.SaveChangesAsync();
	}

	public async ValueTask<int> ExecuteQuery(string commandText, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
	{
		return await ((IDatabaseContext)_dbContext).ExecuteQueryAsync(commandText, commandType, parameters);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposed)
		{
			if (disposing)
			{
				_dbContext.Dispose();
			}
		}
		disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
