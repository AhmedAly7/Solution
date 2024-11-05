using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;

namespace Solution.Data.DbContexts;

public interface IDatabaseContext
{
	DatabaseFacade Database { get; }

	int SaveChanges();

	Task<int> SaveChangesAsync();

	DbSet<TEntity> Set<TEntity>() where TEntity : class;

	Task<IEnumerable<T>> QueryFromSqlAsync<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new();

	IEnumerable<T> QueryFromSql<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new();

	Task<int> ExecuteQueryAsync(string commandText, CommandType commandType, params DbParameter[] parameters);

	int ExecuteQuery(string commandText, CommandType commandType, params DbParameter[] parameters);

	DbDataReader ExecuteDbDataReader(string commandText, CommandType commandType, params DbParameter[] parameters);

	Task<DbDataReader> ExecuteDbDataReaderAsync(string commandText, CommandType commandType, params DbParameter[] parameters);
}