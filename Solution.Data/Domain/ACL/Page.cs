using Solution.Core.Enums.Domain.ACL;

namespace Solution.Data.Domain.ACL;

public class Page
{
	public int Id { get; set; }
	public string NameAr { get; set; }
	public string NameEn { get; set; }
	public Applications ApplicationId { get; set; }
	public UserTypes UserTypeId { get; set; }

	public virtual Application Application { get; set; }
	public virtual UserType UserType { get; set; }
	public virtual List<PagePermission> PagePermissions { get; set; }
}