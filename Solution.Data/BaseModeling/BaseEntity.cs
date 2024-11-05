using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public class BaseEntity : SoftDelete, IBaseEntity
{
	public bool IsActive { get; set; }
}