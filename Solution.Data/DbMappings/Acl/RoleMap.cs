using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Solution.Core.Enums.Domain.ACL;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;

namespace Solution.Data.DbMappings.Acl;

public class RoleMap : CustomEntityTypeConfiguration<Role>
{
	public override void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable("Roles", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
		builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();
		builder.Property(p => p.IsDeleted).HasDefaultValue(false);
		builder.Property(p => p.IsActive).HasDefaultValue(true);

		builder.Property(p => p.UserTypeId).HasDefaultValue(UserTypes.Admin).IsRequired();
		builder.HasOne(p => p.UserType).WithMany().HasForeignKey(fk => fk.UserTypeId).OnDelete(DeleteBehavior.Restrict);

		base.Configure(builder);
	}
}