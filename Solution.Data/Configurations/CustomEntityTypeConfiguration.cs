using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Solution.Data.Configurations;

public class CustomEntityTypeConfiguration<TEntity> : IModelConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class
{
	public void ApplyConfiguration(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(this);
	}

	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		//if (typeof(IBaseEntity).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.HasKey(k => ((IBaseEntity)k).Id);
		//}
		//if (typeof(IBaseLongEntity).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.HasKey(k => ((IBaseLongEntity)k).Id);
		//}
		//if (typeof(IBaseGuidEntity).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.HasKey(k => ((IBaseGuidEntity)k).Id);
		//}
		//if (typeof(IBaseAuditableEntity).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.Property(p => ((IBaseAuditableEntity)p).CreatedAt).IsRequired();
		//}
		//if (typeof(ICanActive).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.Property(p => ((ICanActive)p).IsActive).HasDefaultValue(true);
		//}
		//if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.Property(p => ((ISoftDelete)p).IsDeleted).HasDefaultValue(false);
		//}
		//if (typeof(IHasComment).IsAssignableFrom(typeof(TEntity)))
		//{
		//	builder.Property(p => ((IHasComment)p).Comment).HasMaxLength(500).IsRequired(false);
		//}
	}
}