using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;

namespace Solution.Data.DbMappings.Acl;

public class UserTypeMap : CustomEntityTypeConfiguration<UserType>
{
	public override void Configure(EntityTypeBuilder<UserType> builder)
	{
		builder.ToTable("UserTypes", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.NameAr).IsRequired().HasMaxLength(50);
		builder.Property(p => p.NameEn).IsRequired().HasMaxLength(50);

		base.Configure(builder);
	}
}