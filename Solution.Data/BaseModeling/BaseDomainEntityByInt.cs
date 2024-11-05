using Solution.Core.Interfaces;

namespace Solution.Data.BaseModeling;

public class BaseDomainEntityByInt : DefaultIdAuditableEntity, IBaseEntity
{
	public int Id { get; set; }
}