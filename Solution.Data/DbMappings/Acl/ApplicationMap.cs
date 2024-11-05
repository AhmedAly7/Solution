using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;

namespace Solution.Data.DbMappings.Acl;

public class ApplicationMap : CustomEntityTypeConfiguration<Application>
{
	public override void Configure(EntityTypeBuilder<Application> builder)
	{
		builder.ToTable("Applications", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
		builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();

		base.Configure(builder);
	}
}
