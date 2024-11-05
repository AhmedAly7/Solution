using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public abstract class SoftDelete : ISoftDelete
{
	public int? DeletedBy { get; set; }
	public DateTime? DeletedAt { get; set; }
	public bool IsDeleted { get; set; }
}