using Microsoft.Extensions.Configuration;

namespace Solution.Services.IServices.Configurations;

public interface iConfigService
{
	IConfigurationSection ConfigSection(string section);
}