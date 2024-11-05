using Solution.Core.Enums.Domain.ACL;
using Solution.Data.BaseModeling;

namespace Solution.Data.Domain.ACL;

public class Role : BaseDomainEntity
{
	public Role()
	{
		RolePagePermissions = new HashSet<RolePagePermission>();
	}

	public string NameAr { get; set; }
	public string NameEn { get; set; }
	public UserTypes UserTypeId { get; set; }
	public bool ReadOnly { get; set; }

	public UserType UserType { get; set; }

	public virtual ICollection<RolePagePermission> RolePagePermissions { get; set; }
}