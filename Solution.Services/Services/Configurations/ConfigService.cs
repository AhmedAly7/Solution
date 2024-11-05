using Microsoft.Extensions.Configuration;
using Solution.Services.IServices.Configurations;

namespace Solution.Services.Services.Configurations;

public class ConfigService(IConfiguration configuration) : iConfigService
{
	private readonly IConfiguration Configuration = configuration;

	public IConfigurationSection ConfigSection(string section)
	{
		return Configuration.GetSection(section);
	}
}