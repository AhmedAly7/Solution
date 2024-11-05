using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Solution.Data.DbContexts;

namespace Solution.Data.Configurations;

[DbContext(typeof(DatabaseContext))]
public partial class DbContextDatabaseModelInstence : RuntimeModel
{
	static DbContextDatabaseModelInstence()
	{
		var model = new DbContextDatabaseModelInstence();
		model.Initialize();
		model.Customize();
		_instance = model;
	}
	private static DbContextDatabaseModelInstence _instance;
	public static IModel Instance => _instance;
	partial void Initialize();
	partial void Customize();
}