using Solution.DTO.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Solution.Data.DbContexts;
using Solution.Core.Enums;
using Solution.Core.Common;

namespace Solution.Infrastructure.Repository;

public class GRepository<T>(DatabaseContext dbContext) : IGRepository<T>
		where T : class
{
	#region Private fields

	protected readonly DatabaseContext _dbContext = dbContext;
	private bool _disposed = false;

	#endregion

	#region Find Methods

	/// <summary>
	/// Searches for record(s) using given keys
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public T Find(params object[] keys)
	{
		return _dbContext.Set<T>().Find(keys);
	}

	/// <summary>
	/// Searches for record(s) using given condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public T Find(Func<T, bool> where)
	{
		return _dbContext.Set<T>().Where(where).FirstOrDefault<T>();
	}

	/// <summary>
	/// Searches for record(s) using given keys asynchronously
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public async ValueTask<T> FindAsync(params object[] keys)
	{
		return await _dbContext.Set<T>().FindAsync(keys);
	}

	/// <summary>
	/// Searches for record(s) that match(es) a given condition
	/// </summary>
	/// <param name="match"></param>
	/// <returns></returns>
	public async ValueTask<T> FindAsync(Expression<Func<T, bool>> match)
	{
		return _dbContext.Set<T>().AsNoTracking()?.Where(match)?.FirstOrDefault<T>();
	}

	#endregion

	#region Count Methods

	/// <summary>
	/// Retrieve the count of currently exisiting records
	/// </summary>
	/// <returns></returns>
	public int Count()
	{
		return _dbContext.Set<T>().Count();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="predicate"></param>
	/// <returns></returns>
	public int Count(Expression<Func<T, bool>> predicate)
	{
		return _dbContext.Set<T>().Count(predicate);
	}

	/// <summary>
	/// Retrieve the count of currently exisiting records asynchronously
	/// </summary>
	/// <returns></returns>

	public async ValueTask<int> CountAsync()
	{
		return await _dbContext.Set<T>().CountAsync();
	}

	public async ValueTask<int> CountAsync(Expression<Func<T, bool>> predicate)
	{
		return await _dbContext.Set<T>().CountAsync(predicate);
	}

	#endregion

	#region Get Methods

	/// <summary>
	/// Retrieve all records
	/// </summary>
	/// <returns></returns>
	public virtual IQueryable<T> GetAll()
	{
		return _dbContext.Set<T>().AsNoTracking();
	}

	/// <summary>
	/// Retrieve all records based on a given condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> where)
	{
		return _dbContext.Set<T>().AsNoTracking().Where(where).AsQueryable();
	}

	/// <summary>
	/// Retrieve all records based on a given condition and key
	/// </summary>
	/// <param name="where"></param>
	/// <param name="select"></param>
	/// <returns></returns>
	public virtual IQueryable<object> GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
	{
		var x = _dbContext.Set<T>().AsNoTracking().Where(where).Select(select);
		return x;
	}

	/// <summary>
	/// Retrieve all records asynchronously
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<IQueryable<T>> GetAllAsync()
	{
		var list = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		return list.AsQueryable();
	}

	/// <summary>
	/// Retrieve all records based on a given condition asynchronously
	/// </summary>
	/// <param name="expression"></param>
	/// <returns></returns>
	public virtual async ValueTask<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
	{
		var list = await this._dbContext.Set<T>().AsNoTracking().Where(expression).ToListAsync();
		return list.AsQueryable();
	}

	/// <summary>
	/// Retrieve all records based on a given condition and selector asynchronously
	/// </summary>
	/// <param name="where"></param>
	/// <param name="select"></param>
	/// <returns></returns>
	public virtual async ValueTask<IQueryable<object>> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
	{
		var list = await _dbContext.Set<T>().AsNoTracking().Where(where).Select(select).ToListAsync();
		return list.AsQueryable();
	}

	/// <summary>
	/// Retrieve all records with set of properties
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
	{
		IQueryable<T> queryable = GetAll();
		foreach (Expression<Func<T, object>> includeProperty in includeProperties)
		{
			queryable = queryable.Include<T, object>(includeProperty);
		}

		return queryable;
	}

	/// <summary>
	/// Retrieve all records with set of properties asynchronously
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public async ValueTask<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
	{
		// need more investigation to avoid actual exeution by tolist()
		IQueryable<T> queryable = null;
		await Task.Run(() => {
			queryable = _dbContext.Set<T>().AsNoTracking().AsQueryable();
			foreach (Expression<Func<T, object>> includeProperty in includeProperties)
			{
				queryable = queryable.Include<T, object>(includeProperty);
			}
		}); //await GetAllAsync();

		return queryable;
		// return (await _dbContext.Set<T>().AsNoTracking().Include(includeProperties.ToString()).ToListAsync()).AsQueryable();
	}

	/// <summary>
	/// Retrieve all records with set of properties
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public IQueryable<T> GetAllIncluding(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includeProperties)
	{
		IQueryable<T> queryable = GetAll();
		foreach (Expression<Func<T, object>> includeProperty in includeProperties)
		{
			queryable = queryable.Include<T, object>(includeProperty);
		}

		if (where != null)
		{
			var list = queryable.Where(where).ToList();

			return list.AsQueryable();
		}
		return queryable;
	}

	/// <summary>
	/// Retrieve all records with set of properties asynchronously
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public async ValueTask<IQueryable<T>> GetAllIncludingAsync(Expression<Func<T, bool>> where = null, params Expression<Func<T, object>>[] includeProperties)
	{
		// need more investigation to avoid actual exeution by tolist()
		IQueryable<T> queryable = null;
		await Task.Run(() => {
			queryable = _dbContext.Set<T>().AsNoTracking().AsQueryable();
			foreach (Expression<Func<T, object>> includeProperty in includeProperties)
			{
				queryable = queryable.Include<T, object>(includeProperty);
			}
		}); //await GetAllAsync();

		if (where != null)
		{
			var list = await queryable.Where(where).ToListAsync();
			return list.AsQueryable();
		}
		return queryable;
		// return (await _dbContext.Set<T>().AsNoTracking().Include(includeProperties.ToString()).ToListAsync()).AsQueryable();
	}

	/// <summary>
	/// Retrieve the first record
	/// </summary>
	/// <returns></returns>
	public T GetFirst()
	{
		return _dbContext.Set<T>().AsNoTracking().FirstOrDefault();
	}

	/// <summary>
	/// Retrieve the first record asynchronously
	/// </summary>
	/// <returns></returns>
	public async ValueTask<T> GetFirstAsync()
	{
		return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync();
	}

	/// <summary>
	/// Retrieve the first record based on a given condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public T GetFirst(Expression<Func<T, bool>> where)
	{
		return _dbContext.Set<T>().AsNoTracking().FirstOrDefault(where);
	}

	/// <summary>
	/// Retrieve the first record based on a given condition asynchronously
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public async ValueTask<T> GetFirstAsync(Expression<Func<T, bool>> where)
	{
		return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(where);
	}

	/// <summary>
	/// Retrieve the last record
	/// </summary>
	/// <returns></returns>
	public T GetLast()
	{
		return _dbContext.Set<T>().AsNoTracking().LastOrDefault();
	}

	/// <summary>
	/// Retrieve the last record asynchronously
	/// </summary>
	/// <returns></returns>
	public async ValueTask<T> GetLastAsync()
	{
		return await _dbContext.Set<T>().AsNoTracking().LastOrDefaultAsync();
	}

	/// <summary>
	/// Retrieve the last record based on a given condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public T GetLast(Expression<Func<T, bool>> where)
	{
		return _dbContext.Set<T>().AsNoTracking().LastOrDefault(where);
	}

	/// <summary>
	/// Retrieve the last record based on a given condition asynchronously
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public async ValueTask<T> GetLastAsync(Expression<Func<T, bool>> where)
	{
		return await _dbContext.Set<T>().AsNoTracking().LastOrDefaultAsync(where);
	}

	public bool Any(Expression<Func<T, bool>> predicate)
	{
		return _dbContext.Set<T>().AsNoTracking().Any(predicate);
	}

	public bool Any(Expression<Func<T, bool>> predicate, params string[] includingTables)
	{
		IQueryable<T> entitiesList = _dbContext.Set<T>().AsNoTracking().AsQueryable();

		if (includingTables != null)
			for (int i = 0; i < includingTables.Length; i++)
				entitiesList = entitiesList.Include(includingTables[i]);

		return entitiesList.Any(predicate);
	}

	public async ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate)
	{
		return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
	}

	public async ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate, params string[] includingTables)
	{
		IQueryable<T> entitiesList = _dbContext.Set<T>().AsNoTracking().AsQueryable();

		if (includingTables != null)
			for (int i = 0; i < includingTables.Length; i++)
				entitiesList = entitiesList.Include(includingTables[i]);

		return await entitiesList.AnyAsync(predicate);
	}

	#endregion

	#region Minimum Methods

	/// <summary>
	/// Returns the minimum value of generic IQueryable 
	/// </summary>
	/// <returns></returns>
	public T GetMinimum()
	{
		return _dbContext.Set<T>().Min();
	}

	/// <summary>
	/// Returns the minimum value of generic IQueryable asynchronously
	/// </summary>
	/// <returns></returns>
	public async ValueTask<T> GetMinimumAsync()
	{
		return await _dbContext.Set<T>().MinAsync();
	}

	/// <summary>
	/// Returns the minimum value of generic IQueryable using given key
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public object GetMinimum(Expression<Func<T, object>> selector)
	{
		return _dbContext.Set<T>().Min(selector);
	}

	/// <summary>
	/// Returns the minimum value of generic IQueryable using given key asynchronously
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public async ValueTask<object> GetMinimumAsync(Expression<Func<T, object>> selector)
	{
		return await _dbContext.Set<T>().MinAsync(selector);
	}

	#endregion

	#region Maximum Methods

	/// <summary>
	/// Returns the maximum value of generic IQueryable
	/// </summary>
	/// <returns></returns>
	public T GetMaximum()
	{
		return _dbContext.Set<T>().Max();
	}

	/// <summary>
	/// Returns the maximum value of generic IQueryable asynchronously
	/// </summary>
	/// <returns></returns>
	public async ValueTask<T> GetMaximumAsync()
	{
		return await _dbContext.Set<T>().MaxAsync();
	}

	/// <summary>
	/// Returns the maximum value of generic IQueryable using given key
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public object GetMaximum(Expression<Func<T, object>> selector)
	{
		return _dbContext.Set<T>().Max(selector);
	}

	/// <summary>
	/// Returns the maximum value of generic IQueryable using given key asynchronously
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public async ValueTask<object> GetMaximumAsync(Expression<Func<T, object>> selector)
	{
		return await _dbContext.Set<T>().MaxAsync(selector);
	}

	#endregion

	#region Pagination Methods

	/// <summary>
	/// Paginate the retrieved records besed on specified condition with specified set of properties and specified key used in order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <param name="predicate"></param>
	/// <param name="orderByType"></param>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	private PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, OrderByType orderByType, params Expression<Func<T, object>>[] includeProperties)
	{

		IQueryable<T> queryable =
			(orderByType == OrderByType.Ascending)
				? GetAllIncluding(includeProperties: includeProperties).AsQueryable().OrderBy(keySelector)
				: GetAllIncluding(includeProperties: includeProperties).AsQueryable().OrderByDescending(keySelector);

		queryable = (predicate != null) ? queryable.Where(predicate) : queryable;
		PaginatedList<T> paginatedList = queryable.ToPaginatedList(pageIndex, pageSize);

		return paginatedList;
	}

	/// <summary>
	/// Paginate the retrieved records besed with specified set of properties and specified key used in order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <returns></returns>
	public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
	{
		return Paginate<TKey>(pageIndex, pageSize, keySelector, null);
	}

	/// <summary>
	/// Paginate the retrieved records besed on specified condition with specified set of properties and specified key used in order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <param name="predicate"></param>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
	{

		PaginatedList<T> paginatedList = Paginate<TKey>(
			pageIndex, pageSize, keySelector, predicate, OrderByType.Ascending, includeProperties);

		return paginatedList;
	}

	/// <summary>
	/// Paginate the retrieved records with specified set of properties and specified key used in Descending order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <returns></returns>
	public PaginatedList<T> PaginateDescending<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
	{
		return PaginateDescending<TKey>(pageIndex, pageSize, keySelector, null);
	}

	/// <summary>
	/// Paginate the retrieved records based on specified condition and specified set of properties and specified key used in Descending order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <param name="predicate"></param>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public PaginatedList<T> PaginateDescending<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
	{
		PaginatedList<T> paginatedList = Paginate<TKey>(
			pageIndex, pageSize, keySelector, predicate, OrderByType.Descending, includeProperties);

		return paginatedList;
	}

	public Task<IEnumerable<T>> FindAsync(BaseSearchModel searchModel, Expression<Func<T, bool>> predicate)
	{
		return
			 Task.Run(async () =>
			 {
				 IQueryable<T> entitiesList = GetAll().AsQueryable();

				 int start = (searchModel.PageIndex - 1) * searchModel.PageSize;

				 entitiesList = entitiesList
					 .Where(predicate)
					 .OrderBy(searchModel.SortBy)
					 .Skip(start)
					 .Take(searchModel.PageSize);

				 searchModel.TotalRowsCount = await CountAsync(predicate);
				 searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

				 return entitiesList.AsEnumerable<T>();
			 }
				 );
	}

	public Task<IEnumerable<T>> FindAsync(BaseSearchModel searchModel, Expression<Func<T, bool>> predicate, params string[] includingTables)
	{
		return
		Task.Run(async () =>
		{
			IQueryable<T> entitiesList = GetAll().AsQueryable();

			if (includingTables != null)
				for (int i = 0; i < includingTables.Length; i++)
					entitiesList = entitiesList.Include(includingTables[i]);

			int start = (searchModel.PageIndex - 1) * searchModel.PageSize;

			entitiesList = entitiesList
				.Where(predicate)
				.OrderBy(searchModel.SortBy)
				.Skip(start)
				.Take(searchModel.PageSize);

			searchModel.TotalRowsCount = await CountAsync(predicate);
			searchModel.TotalPages = (int)Math.Ceiling((double)searchModel.TotalRowsCount / searchModel.PageSize);

			return entitiesList.AsEnumerable<T>();
		}
			);
	}

	#endregion

	#region Add Methods

	/// <summary>
	/// Insert single entity 
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual object Add(T entity)
	{
		return _dbContext.Set<T>().Add(entity).Entity;
	}

	/// <summary>
	/// Insert single entity asynchronously
	/// </summary>
	/// <param name="t"></param>
	/// <returns></returns>
	public virtual async ValueTask AddAsync(T t)
	{
		await _dbContext.Set<T>().AddAsync(t);
	}

	/// <summary>
	/// Insert list of entities
	/// </summary>
	/// <param name="entities"></param>
	public virtual void AddRange(IEnumerable<T> entities)
	{
		_dbContext.Set<T>().AddRange(entities);
	}

	/// <summary>
	/// Insert list of entities asynchronously
	/// </summary>
	/// <param name="entities"></param>
	/// <returns></returns>
	public virtual async ValueTask AddRangeAsync(IEnumerable<T> entities)
	{
		await _dbContext.Set<T>().AddRangeAsync(entities);
	}

	#endregion

	#region Remove Methods

	/// <summary>
	/// Logically or physically deleting record based on the entity type
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual EntityEntry<T> Remove(T entity)
	{
		return _dbContext.Update(entity);
	}

	/// <summary>
	/// Logically or physically deleting list of records based on the entity type
	/// </summary>
	/// <param name="entities"></param>
	public virtual void RemoveRange(IEnumerable<T> entities)
	{
		_dbContext.Set<T>().RemoveRange(entities);
	}

	public virtual void Truncate()
	{
		var sqlCmd = $"TRUNCATE TABLE {typeof(T).Name}" + "s";
		int DatabaseNumber = _dbContext.Database.ExecuteSqlRaw(sqlCmd);
	}

	#endregion

	#region Update Method

	/// <summary>
	/// Update record data
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public virtual EntityEntry<T> Update(T entity)
	{
		this._dbContext.Entry(entity).State = EntityState.Detached;
		return _dbContext.Update(entity);
	}

	public virtual void UpdateRange(IEnumerable<T> entities)
	{
		_dbContext.Set<T>().UpdateRange(entities);
	}

	#endregion

	#region Helper Methods

	/// <summary>
	/// Generic filter
	/// </summary>
	/// <typeparam name="TProperty"></typeparam>
	/// <param name="dbSet"></param>
	/// <param name="property"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	private IQueryable<T> Filter<TProperty>(IQueryable<T> dbSet,
		Expression<Func<T, TProperty>> property, TProperty value)
		where TProperty : IComparable
	{

		var memberExpression = property.Body as MemberExpression;
		if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
		{

			throw new ArgumentException("Property expected", "property");
		}

		Expression left = property.Body;
		Expression right = Expression.Constant(value, typeof(TProperty));
		Expression searchExpression = Expression.Equal(left, right);
		Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(
			searchExpression, new ParameterExpression[] { property.Parameters.Single() });

		return dbSet.Where(lambda);
	}

	#endregion

	#region Release Unmanaged Resources

	/// <summary>
	/// Release un managed resources from memeory
	/// </summary>
	/// <param name="disposing"></param>
	public void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_dbContext.Dispose();
				GC.SuppressFinalize(this);
			}
		}
		_disposed = true;
	}

	#endregion
}

