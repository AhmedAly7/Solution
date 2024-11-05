using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Infrastructures;
using Solution.Data.BaseModeling;
using Solution.Data.Configurations;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace Solution.Data.DbContexts;

public class DatabaseContext : DbContext, IDatabaseContext
{
	readonly IUserProvider _userProvider;
	public DatabaseContext(DbContextOptions<DatabaseContext> optionsBuilder,
			IUserProvider userProvider)
		: base(optionsBuilder)
	{
		ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		_userProvider = userProvider;
	}

	#region Overriden

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	public override int SaveChanges()
	{
		SaveAuditFields();
		return base.SaveChanges();
	}

	public Task<int> SaveChangesAsync()
	{
		SaveAuditFields();
		return base.SaveChangesAsync();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		RegisterDomainModels(modelBuilder);
		base.OnModelCreating(modelBuilder);
	}

	#endregion Overriden

	#region Sql Commands

	public int ExecuteQuery(string commandText, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
	{
		DbConnection cn = this.Database.GetDbConnection();
		DbCommand cmd = cn.CreateCommand();

		cmd.CommandText = commandText;
		cmd.CommandType = commandType;

		if (parameters != null && parameters.Length > 0)
			cmd.Parameters.AddRange(parameters);

		if (cn.State != ConnectionState.Open)
			cn.Open();

		return cmd.ExecuteNonQuery();
	}

	public async Task<int> ExecuteQueryAsync(string commandText, CommandType commandType = CommandType.Text, params DbParameter[] parameters)
	{
		DbConnection cn = this.Database.GetDbConnection();
		DbCommand cmd = cn.CreateCommand();

		cmd.CommandText = commandText;
		cmd.CommandType = commandType;

		if (parameters != null && parameters.Length > 0)
			cmd.Parameters.AddRange(parameters);

		if (cn.State != ConnectionState.Open)
			await cn.OpenAsync();

		var result = await cmd.ExecuteNonQueryAsync();

		if (cn.State == ConnectionState.Open)
			await cn.CloseAsync();
		return result;
	}

	public IEnumerable<T> QueryFromSql<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new()
	{
		using (DbDataReader reader = this.ExecuteDbDataReader(commandText, commandType, parameters))
		{
			if (!reader.HasRows)
				return null;

			T entity = new T();
			List<T> entities = new List<T>();

			Type entityType = entity.GetType();

			while (reader.Read())
			{
				entity = new T();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					PropertyInfo propertyInfo = entityType.GetProperty(reader.GetName(i));

					if (propertyInfo != null)
						propertyInfo.SetValue(entity, reader[i] == DBNull.Value ? null : reader[i]);
				}
				entities.Add(entity);
			}

			return entities;
		}
	}

	public async Task<IEnumerable<T>> QueryFromSqlAsync<T>(string commandText, CommandType commandType, params DbParameter[] parameters) where T : class, new()
	{
		using (DbDataReader reader = await ExecuteDbDataReaderAsync(commandText, commandType, parameters))
		{
			if (!reader.HasRows)
				return null;

			T entity = new T();
			List<T> entities = new List<T>();

			Type entityType = entity.GetType();

			while (reader.Read())
			{
				entity = new T();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					PropertyInfo propertyInfo = entityType.GetProperty(reader.GetName(i));

					if (propertyInfo != null)
						propertyInfo.SetValue(entity, reader[i] == DBNull.Value ? null : reader[i]);
				}
				entities.Add(entity);
			}

			return entities;
		}
	}

	public DbDataReader ExecuteDbDataReader(string commandText, CommandType commandType, params DbParameter[] parameters)
	{
		DbConnection cn = this.Database.GetDbConnection();
		DbCommand cmd = cn.CreateCommand();

		cmd.CommandText = commandText;
		cmd.CommandType = commandType;

		if (parameters != null && parameters.Length > 0)
			cmd.Parameters.AddRange(parameters);

		if (cn.State != ConnectionState.Open)
			cn.Open();

		return cmd.ExecuteReader();
	}

	public Task<DbDataReader> ExecuteDbDataReaderAsync(string commandText, CommandType commandType, params DbParameter[] parameters)
	{
		DbConnection cn = this.Database.GetDbConnection();
		DbCommand cmd = cn.CreateCommand();

		cmd.CommandText = commandText;
		cmd.CommandType = commandType;

		if (parameters != null && parameters.Length > 0)
			cmd.Parameters.AddRange(parameters);

		if (cn.State != ConnectionState.Open)
			cn.Open();

		return cmd.ExecuteReaderAsync();
	}

	#endregion Sql Commands

	#region Methods

	private void RegisterDomainModels(ModelBuilder modelBuilder)
	{
		Assembly.GetExecutingAssembly().GetTypes()
			.Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == (typeof(CustomEntityTypeConfiguration<>)))
			.ToList()
			.ForEach(map =>
			{
				if ((Activator.CreateInstance(map) as IModelConfiguration) != null)
					((IModelConfiguration)Activator.CreateInstance(map)).ApplyConfiguration(modelBuilder);
			});
	}

	public void SaveAuditFields()
	{
		// Need to refacotor and check
		var BaseDomainEntityAuditedEntries = ChangeTracker
			.Entries().Where(e => (e.Entity is BaseDomainEntity)
			&& (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));
		foreach (var entityEntry in BaseDomainEntityAuditedEntries)
		{
			if (entityEntry.State == EntityState.Added)
			{
				entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
				entityEntry.Property("CreatedBy").CurrentValue = _userProvider?.UserId;
				entityEntry.Property("CreatedAt").IsModified = true;
				entityEntry.Property("CreatedBy").IsModified = true;
				//((BaseDomainEntity)entityEntry.Entity).IsActive = true;
				//((BaseDomainEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
				//((BaseDomainEntity)entityEntry.Entity).CreatedBy = _userProvider?.UserId;
			}
			else if (entityEntry.State == EntityState.Modified)
			{
				entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow; ;
				entityEntry.Property("UpdatedBy").CurrentValue = _userProvider?.UserId;
				entityEntry.Property("UpdatedAt").IsModified = true;
				entityEntry.Property("UpdatedBy").IsModified = true;

				//((BaseDomainEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
				//((BaseDomainEntity)entityEntry.Entity).UpdatedBy = _userProvider?.UserId;
				Entry((BaseDomainEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
				Entry((BaseDomainEntity)entityEntry.Entity).Property(p => p.CreationDate).IsModified = false;
			}
			else if (entityEntry.State == EntityState.Deleted)
			{
				entityEntry.State = EntityState.Unchanged;
				if (((BaseDomainEntity)entityEntry.Entity).IsDeleted)
				{
					((BaseDomainEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
					((BaseDomainEntity)entityEntry.Entity).DeletedBy = _userProvider?.UserId;
					((BaseDomainEntity)entityEntry.Entity).IsDeleted = true;
				}
				else
				{
					((BaseDomainEntity)entityEntry.Entity).DeletedAt = null;
					((BaseDomainEntity)entityEntry.Entity).DeletedBy = null;
					((BaseDomainEntity)entityEntry.Entity).IsDeleted = false;
				}

				entityEntry.Property("DeletedAt").IsModified = true;
				entityEntry.Property("DeletedBy").IsModified = true;
				entityEntry.Property("IsDeleted").IsModified = true;
			}

		}
		var BaseLookupWithAuditEntityAuditedEntries = ChangeTracker
			.Entries().Where(e => (e.Entity is DefaultIdAuditableEntity)
			&& (e.State == EntityState.Added || e.State == EntityState.Modified));
		foreach (var entityEntry in BaseLookupWithAuditEntityAuditedEntries)
		{
			if (entityEntry.State == EntityState.Added)
			{
				//((DefaultIdAuditableEntity)entityEntry.Entity).IsActive = true;
				((DefaultIdAuditableEntity)entityEntry.Entity).CreationDate = DateTime.UtcNow;
				((DefaultIdAuditableEntity)entityEntry.Entity).CreatedBy = _userProvider?.UserId;
			}
			else
			{
				((DefaultIdAuditableEntity)entityEntry.Entity).LastUpdateDate = DateTime.UtcNow;
				((DefaultIdAuditableEntity)entityEntry.Entity).LastUpdatedBy = _userProvider?.UserId;
				Entry((DefaultIdAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
				Entry((DefaultIdAuditableEntity)entityEntry.Entity).Property(p => p.CreationDate).IsModified = false;
			}
		}
	}

	#endregion Methods
}