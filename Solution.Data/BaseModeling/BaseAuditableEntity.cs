using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public class BaseAuditableEntity : BaseEntity, IAuditable
{
	public int? CreatedBy { get; set; }
	public DateTime? CreationDate { get; set; }
	public int? LastUpdatedBy { get; set; }
	public DateTime? LastUpdateDate { get; set; }

}