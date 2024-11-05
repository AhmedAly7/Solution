using Solution.Core.Common;
using System.Linq.Expressions;

namespace Solution.Services.IServices.Generic;

public interface IGService<T> where T : class
{
	#region Add Method
	void Add(T Model);
	ValueTask AddAsync(T Object);
	void AddRange(IEnumerable<T> Objects);
	ValueTask AddRangeAsync(IEnumerable<T> Objects);

	#endregion

	#region Count Methods
	int Count();
	ValueTask<int> CountAsync();

	#endregion

	#region Update Method
	/// <summary> 
	/// Updates entity within the the repository 
	/// </summary> 
	/// <param name="entity">the entity to update</param> 
	/// <returns>The updates entity</returns> 
	T Update(T entity);


	#endregion

	#region Delete Method
	/// <summary> 
	/// Mark entity to be deleted within the repository 
	/// </summary> 
	/// <param name="entity">The entity to delete</param> 
	T Remove(T entity);
	IEnumerable<T> RemoveRange(IEnumerable<T> Objects);
	#endregion

	#region Find Methods
	T Find(params object[] keys);
	ValueTask<T> FindAsync(params object[] keys);
	T Find(Func<T, bool> where);
	ValueTask<T> FindAsync(Expression<Func<T, bool>> match);
	#endregion

	#region Get Methods

	IEnumerable<T> GetAll();
	ValueTask<IEnumerable<T>> GetAllAsync();
	IEnumerable<T> GetAll(Expression<Func<T, bool>> where);
	ValueTask<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where);
	IEnumerable<T> GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
	ValueTask<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select);
	IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
	ValueTask<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
	T GetFirstOrDefault();
	ValueTask<T> GetFirstOrDefaultAsync();
	T GetLastOrDefault();
	ValueTask<T> GetLastOrDefaultAsync();
	T GetFirstOrDefault(Expression<Func<T, bool>> where);
	ValueTask<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where);
	T GetLastOrDefault(Expression<Func<T, bool>> where);
	ValueTask<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> where);
	#endregion

	#region Pagination Methods

	PaginatedList<T> Paginate<TKey>(
		int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

	PaginatedList<T> PaginateDescending<TKey>(
		int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector);

	PaginatedList<T> PaginateDescending<TKey>(
		int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

	#endregion

	#region Aggregation Methods
	T GetMinimum();
	ValueTask<T> GetMinimumAsync();
	object GetMinimum(Expression<Func<T, object>> selector);
	ValueTask<object> GetMinimumAsync(Expression<Func<T, object>> selector);

	T GetMaximum();
	ValueTask<T> GetMaximumAsync();
	object GetMaximum(Expression<Func<T, object>> selector);
	ValueTask<object> GetMaximumAsync(Expression<Func<T, object>> selector);
	#endregion

	string GenerateCode(string prefix);
}