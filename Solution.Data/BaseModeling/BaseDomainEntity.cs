using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public abstract class BaseDomainEntity : DefaultIdAuditableEntity, IBaseEntity
{
	public long Id { get; set; }
}