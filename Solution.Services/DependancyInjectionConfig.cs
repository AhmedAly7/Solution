using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solution.Core.Infrastructures;
using Solution.Data.Factories;
using Solution.Infrastructure.Repository;
using Solution.Infrastructure.UOW;
using Solution.Services.IServices.Configurations;
using Solution.Services.IServices.Generic;
using Solution.Services.IServices.Helpers;
using Solution.Services.IServices.Infrastructure;
using Solution.Services.Services.Configurations;
using Solution.Services.Services.Generic;
using Solution.Services.Services.Helpers;
using Solution.Services.Services.Infrastructure;

namespace Solution.Services;

public static class DependancyInjectionConfig
{
	public static IServiceProvider ServiceProvider { get; set; }

	public static ServiceProvider Config(IServiceCollection services, IConfiguration configuration)
	{
		#region DbContext ...

		services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

		#endregion DbContext ...

		#region Repositories ...

		services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));

		#endregion Repositories ...

		#region Services ...

		services.AddScoped(typeof(IGService<>), typeof(GService<>));
		services.AddScoped<iConfigService, ConfigService>();
		services.AddScoped<IParameterFactory, ParameterFactory>();
		services.AddScoped<IEncryptionProvider, EncryptionProvider>();
		services.AddScoped<IExportReportServiceHelper, ExportReportServiceHelper>();

		#endregion Services ...

		ServiceProvider = services.BuildServiceProvider();
		return (ServiceProvider as ServiceProvider);
	}
}