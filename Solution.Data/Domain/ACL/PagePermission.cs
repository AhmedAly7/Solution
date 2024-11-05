using Solution.Core.Enums.Domain.ACL;

namespace Solution.Data.Domain.ACL;

public class PagePermission
{
	public int Id { get; set; }
	public PermissionsTerms PermissionTermId { get; set; }
	public int PageId { get; set; }
	public string ACLName { get; set; }
	public virtual PermissionsTerm PermissionsTerm { get; set; }
	public virtual Page Page { get; set; }
}