using Solution.Core.Enums.Domain.ACL;
using Solution.Data.Domain.ACL;

namespace Solution.DTO.Models.Acl;

public class SystemPageFormDto
{
	public long Id { get; set; }
	public string NameAr { get; set; }
	public string NameEn { get; set; }
	public byte ApplicationId { get; set; }
	public UserTypes UserTypeId { get; set; }
	public Application Application { get; set; }
	public List<SystemPagePermissionDto> PagePermissions { get; set; }
}
