namespace Solution.Data.Domain.ACL;

public class RolePagePermission
{
	public int Id { get; set; }
	public int PagePermissionId { get; set; }
	public long RoleId { get; set; }
	public virtual PagePermission PagePermission { get; set; }
	public virtual Role Role { get; set; }
}