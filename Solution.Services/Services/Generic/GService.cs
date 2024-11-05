using Solution.Core.Common;
using Solution.Core.Interfaces;
using Solution.Data.DbContexts;
using Solution.Infrastructure.Repository;
using Solution.Infrastructure.UOW;
using Solution.Services.IServices.Generic;
using System.Linq.Expressions;

namespace Solution.Services.Services.Generic;

public class GService<T>(IGRepository<T> genericRepository, IUnitOfWork<DatabaseContext> unitOfWork) : IGService<T>
	   where T : class
{
	#region Private Fields

	protected readonly IGRepository<T> _genericRepository = genericRepository;
	protected readonly IUnitOfWork<DatabaseContext> _unitOfWork = unitOfWork;

	#endregion

	#region Add Method

	/// <summary>
	/// Insert a single entity
	/// </summary>
	/// <param name="Object"></param>
	/// <returns></returns>
	public virtual void Add(T Object)
	{
		try
		{
			if (Object is IAuditable)
			{
				((IAuditable)Object).CreationDate = DateTime.UtcNow;
			}

			_genericRepository.Add(Object);

			_unitOfWork.SaveChanges();
		}
		catch (Exception ex)
		{
		}
	}

	/// <summary>
	/// Insert a single entity asynchronously
	/// </summary>
	/// <param name="Object"></param>
	/// <returns></returns>
	public virtual async ValueTask AddAsync(T Object)
	{
		try
		{
			if (Object is IAuditable)
			{
				((IAuditable)Object).CreationDate = DateTime.UtcNow;
			}

			await _genericRepository.AddAsync(Object);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception ex)
		{
		}
	}

	/// <summary>
	/// Insert a list of entities
	/// </summary>
	/// <param name="Objects"></param>
	/// <returns></returns>
	public virtual void AddRange(IEnumerable<T> Objects)
	{
		try
		{
			if (Objects != null && Objects.Count() > 0)
			{
				foreach (T Object in Objects)
				{
					if (Object is IAuditable)
						((IAuditable)Object).CreationDate = DateTime.UtcNow;
				}
			}

			_genericRepository.AddRange(Objects);
			_unitOfWork.SaveChanges();
		}
		catch (Exception ex)
		{
		}
	}

	/// <summary>
	/// Insert a list of entities asynchronously
	/// </summary>
	/// <param name="Objects"></param>
	/// <returns></returns>
	public virtual async ValueTask AddRangeAsync(IEnumerable<T> Objects)
	{
		try
		{
			if (Objects != null && Objects.Count() > 0)
			{
				foreach (T Object in Objects)
				{
					if (Object is IAuditable)
						((IAuditable)Object).CreationDate = DateTime.UtcNow;
				}
			}

			await _genericRepository.AddRangeAsync(Objects);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception ex)
		{
		}
	}

	#endregion

	#region Count Methods

	/// <summary>
	/// Retrieve the count of currently exisiting records 
	/// </summary>
	/// <returns></returns>
	public virtual int Count()
	{
		try
		{
			return _genericRepository.Count();
		}
		catch (Exception ex)
		{
			return 0;
		}
	}

	/// <summary>
	/// Retrieve the count of currently exisiting records asynchronously
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<int> CountAsync()
	{
		try
		{
			return await _genericRepository.CountAsync();
		}
		catch (Exception ex)
		{
			return 0;
		}
	}

	#endregion

	#region Update Method

	/// <summary>
	/// Update record data
	/// </summary>
	/// <param name="Object"></param>
	/// <returns></returns>
	public virtual T Update(T Object)
	{
		try
		{
			_genericRepository.Update(Object);
			return Object;
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	#endregion

	#region Delete Method

	/// <summary>
	/// Soft delete of records by updating is deleted property to be true
	/// </summary>
	/// <param name="Object"></param>
	/// <returns></returns>
	public virtual T Remove(T Object)
	{
		try
		{
			if (Object is ISoftDelete)
			{
				((ISoftDelete)Object).IsDeleted = true;

				if (Object is IAuditable)
				{
					((IAuditable)Object).LastUpdateDate = DateTime.UtcNow;
				}
			}

			var result = _genericRepository.Update(Object);

			return Object;
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Logically or physically deleting list of records based on the entity type
	/// </summary>
	/// <param name="Objects"></param>
	/// <returns></returns>
	public virtual IEnumerable<T> RemoveRange(IEnumerable<T> Objects)
	{
		try
		{

			if (Objects != null && Objects.Count() > 0)
			{
				if (Objects.First() is ISoftDelete)
				{
					foreach (T entityObject in Objects)
					{
						((ISoftDelete)entityObject).IsDeleted = true;
						if (entityObject is IAuditable)
						{
							((IAuditable)entityObject).LastUpdateDate = DateTime.UtcNow;
						}
						_genericRepository.Update(entityObject);
					}
				}
				else
				{
					_genericRepository.RemoveRange(Objects);
				}

				return Objects;
			}
			return default(IEnumerable<T>);
		}
		catch (Exception ex)
		{
			return default(IEnumerable<T>);
		}
	}

	#endregion

	#region Find Methods

	/// <summary>
	/// Searches for a record with a specified primary key values
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public virtual T Find(params object[] keys)
	{
		try
		{
			return _genericRepository.Find(keys);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Searches for a record with a specified primary key values asynchronously
	/// </summary>
	/// <param name="keys"></param>
	/// <returns></returns>
	public virtual async ValueTask<T> FindAsync(params object[] keys)
	{
		try
		{
			return await _genericRepository.FindAsync(keys);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Searches for a record with a specified condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual T Find(Func<T, bool> where)
	{
		try
		{
			return _genericRepository.Find(where);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Searches for a record or list of records that match(es) a specified condition asynchronously
	/// </summary>
	/// <param name="match"></param>
	/// <returns></returns>
	public virtual async ValueTask<T> FindAsync(Expression<Func<T, bool>> match)
	{
		try
		{
			return await _genericRepository.FindAsync(match);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	#endregion

	#region Get Methods

	/// <summary>
	///  Retrieve a list of records based on a specified condition
	/// </summary>
	/// <param name="whereCondition"></param>
	/// <returns></returns>
	public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
	{
		try
		{
			return _genericRepository.GetAll(whereCondition);
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	///  Retrieve a list of records 
	/// </summary>
	/// <returns></returns>
	public virtual IEnumerable<T> GetAll()
	{
		try
		{
			return _genericRepository.GetAll();
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	///  Retrieve a list of records based on a specified condition with a specifed set of properties
	/// </summary>
	/// <param name="where"></param>
	/// <param name="select"></param>
	/// <returns></returns>
	public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
	{
		try
		{
			return (IEnumerable<T>)_genericRepository.GetAll(where, select);
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	///  Retrieve a list of records asynchronously
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<IEnumerable<T>> GetAllAsync()
	{
		try
		{
			return await _genericRepository.GetAllAsync();
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	///  Retrieve a list of records based on a specified condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual async ValueTask<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
	{
		try
		{
			return await _genericRepository.GetAllAsync(where);
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	/// Retrieve a list of records based on a specified condition with a specifed set of properties
	/// </summary>
	/// <param name="where"></param>
	/// <param name="select"></param>
	/// <returns></returns>
	public virtual async ValueTask<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> select)
	{
		try
		{
			var result = await _genericRepository.GetAllAsync(where, select);
			return (IEnumerable<T>)result;
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	/// Retrieve a list of records with a specifed set of properties
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
	{
		try
		{
			return _genericRepository.GetAllIncluding(includeProperties);
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	/// Retrieve a list of records with a specifed set of properties asynchronously
	/// </summary>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public virtual async ValueTask<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
	{
		try
		{
			var result = await _genericRepository.GetAllIncludingAsync(includeProperties);
			return (IEnumerable<T>)result;
		}
		catch (Exception ex)
		{
			return Enumerable.Empty<T>();
		}
	}

	/// <summary>
	/// Retrieve the first record
	/// </summary>
	/// <returns></returns>
	public virtual T GetFirstOrDefault()
	{
		try
		{
			return _genericRepository.GetFirst();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the first record based on specified condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual T GetFirstOrDefault(Expression<Func<T, bool>> where)
	{
		try
		{
			return _genericRepository.GetFirst(where);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the first record with asynchronous execution
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<T> GetFirstOrDefaultAsync()
	{
		try
		{
			return await _genericRepository.GetFirstAsync();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the first record based on specified condition with asynchronous execution
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual async ValueTask<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where)
	{
		try
		{
			return await _genericRepository.GetFirstAsync(where);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the last record
	/// </summary>
	/// <returns></returns>
	public virtual T GetLastOrDefault()
	{
		try
		{
			return _genericRepository.GetLast();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the last record based on specified condition
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual T GetLastOrDefault(Expression<Func<T, bool>> where)
	{
		try
		{
			return _genericRepository.GetLast(where);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the last record with asynchronous execution
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<T> GetLastOrDefaultAsync()
	{
		try
		{
			return await _genericRepository.GetLastAsync();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the last record based on specified condition with asynchronous execution
	/// </summary>
	/// <param name="where"></param>
	/// <returns></returns>
	public virtual async ValueTask<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> where)
	{
		try
		{
			return await _genericRepository.GetLastAsync(where);
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	#endregion

	#region Pagination Methods

	/// <summary>
	/// Paginate the retrieved records besed on specific conditions with specified set of properties and specified key used in order
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <param name="predicate"></param>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public virtual PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
	{
		try
		{
			return _genericRepository.Paginate(pageIndex, pageSize, keySelector, predicate);
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	/// <summary>
	/// Paginate the retrieved records with specified set of properties and specified key used in Descending order
	/// </summary>
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <returns></returns>
	public virtual PaginatedList<T> PaginateDescending<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
	{
		try
		{
			return _genericRepository.PaginateDescending(pageIndex, pageSize, keySelector);
		}
		catch (Exception ex)
		{
			return null;
		}
	}

	/// <summary>
	/// Paginate the retrieved records based on specified condition and specified set of properties and specified key used in Descending order
	/// </summary>
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <param name="pageIndex"></param>
	/// <param name="pageSize"></param>
	/// <param name="keySelector"></param>
	/// <param name="predicate"></param>
	/// <param name="includeProperties"></param>
	/// <returns></returns>
	public virtual PaginatedList<T> PaginateDescending<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
	{
		try
		{
			return _genericRepository.PaginateDescending(pageIndex, pageSize, keySelector, predicate, includeProperties);
		}
		catch (Exception ex)
		{
			return null;
		}
	}
	#endregion

	#region GetMinimum Methods
	/// <summary>
	///  Retrieve the minimum of generic list
	/// </summary>
	/// <returns></returns>
	public virtual T GetMinimum()
	{
		try
		{
			return _genericRepository.GetMinimum();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the minimum of generic list asynchronusly
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<T> GetMinimumAsync()
	{
		try
		{
			return await _genericRepository.GetMinimumAsync();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the minimum of generic list using a given key
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public virtual object GetMinimum(Expression<Func<T, object>> selector)
	{
		try
		{
			return _genericRepository.GetMinimum(selector);
		}
		catch (Exception ex)
		{
			return default;
		}
	}

	/// <summary>
	/// Retrieve the minimum of generic list using a given key asynchronously
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public virtual async ValueTask<object> GetMinimumAsync(Expression<Func<T, object>> selector)
	{
		try
		{
			return await _genericRepository.GetMinimumAsync(selector);
		}
		catch (Exception ex)
		{
			return default;
		}
	}
	#endregion

	#region GetMaximum Methods

	/// <summary>
	/// Retrieve the maximum of generic list
	/// </summary>
	/// <returns></returns>
	public virtual T GetMaximum()
	{
		try
		{
			return _genericRepository.GetMaximum();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the maximum of generic list asynchronously
	/// </summary>
	/// <returns></returns>
	public virtual async ValueTask<T> GetMaximumAsync()
	{
		try
		{
			return await _genericRepository.GetMaximumAsync();
		}
		catch (Exception ex)
		{
			return default(T);
		}
	}

	/// <summary>
	/// Retrieve the maximum of generic list using a given key
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public virtual object GetMaximum(Expression<Func<T, object>> selector)
	{
		try
		{
			return _genericRepository.GetMaximum(selector);
		}
		catch (Exception ex)
		{
			return default;
		}
	}

	/// <summary>
	/// Retrieve the maximum of generic list using a given key asynchronously
	/// </summary>
	/// <param name="selector"></param>
	/// <returns></returns>
	public virtual async ValueTask<object> GetMaximumAsync(Expression<Func<T, object>> selector)
	{
		try
		{
			return await _genericRepository.GetMaximumAsync(selector);
		}
		catch (Exception ex)
		{
			return default;
		}
	}

	#endregion

	public virtual string GenerateCode(string prefix)
	{

		return string.Format($"{prefix}#{DateTime.UtcNow.ToString("MMddmmssff")}");
	}
	public virtual string GenerateCodeNumber(string prefix)
	{
		return string.Format($"{prefix}{DateTime.UtcNow.ToString("MMddmmssff")}");
	}
}