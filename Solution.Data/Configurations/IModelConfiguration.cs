using Microsoft.EntityFrameworkCore;

namespace Solution.Data.Configurations;

public interface IModelConfiguration
{
	void ApplyConfiguration(ModelBuilder modelBuilder);
}

