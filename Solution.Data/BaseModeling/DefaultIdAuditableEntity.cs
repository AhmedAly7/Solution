using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public class DefaultIdAuditableEntity : BaseEntity, IAuditable
{
	[Required]
	[ForeignKey("Creator")]
	public int? CreatedBy { get; set; }
	public DateTime? CreationDate { get; set; }
	public int? LastUpdatedBy { get; set; }
	public DateTime? LastUpdateDate { get; set; }
}