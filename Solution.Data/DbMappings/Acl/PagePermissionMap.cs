using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Data.DbMappings.Acl;

public class PagePermissionMap : CustomEntityTypeConfiguration<PagePermission>
{
	public override void Configure(EntityTypeBuilder<PagePermission> builder)
	{
		builder.ToTable("PagePermissions", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
		builder.HasOne(r => r.PermissionsTerm).WithMany().HasForeignKey(f => f.PermissionTermId).IsRequired();
		builder.HasOne(r => r.Page).WithMany(x => x.PagePermissions).HasForeignKey(f => f.PageId).IsRequired();
		builder.Property(r => r.ACLName).HasMaxLength(50).IsRequired();




		base.Configure(builder);
	}
}
