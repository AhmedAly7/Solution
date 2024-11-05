using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Data.Configurations;
using Solution.Data.Domain.ACL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solution.Data.DbMappings.Acl;

public class PageMap : CustomEntityTypeConfiguration<Page>
{
	public override void Configure(EntityTypeBuilder<Page> builder)
	{
		builder.ToTable("Pages", "Acl");
		builder.HasKey(k => k.Id);
		builder.Property(p => p.Id).ValueGeneratedNever().HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
		builder.Property(p => p.NameAr).HasMaxLength(50).IsRequired();
		builder.Property(p => p.NameEn).HasMaxLength(50).IsRequired();
		builder.HasOne(r => r.UserType).WithMany().HasForeignKey(f => f.UserTypeId);
		builder.HasOne(r => r.Application).WithMany().HasForeignKey(f => f.ApplicationId);
		base.Configure(builder);
	}
}