using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Data.DbMappings.Acl;

public class PermissionsTermMap : CustomEntityTypeConfiguration<PermissionsTerm>
{
	public override void Configure(EntityTypeBuilder<PermissionsTerm> builder)
	{
		builder.ToTable("PermissionsTerms", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
		builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
		builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();
		base.Configure(builder);
	}
}
