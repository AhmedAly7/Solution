using Solution.Core.Enums.Domain.ACL;

namespace Solution.DTO.Models.Acl;

public class RoleDto
{
	public long Id { get; set; }
	public string NameAr { get; set; }
	public string NameEn { get; set; }
	public bool ReadOnly { get; set; }
	public UserTypes UserTypeId { get; set; }
	public string UserTypeNameAr { get; set; }
	public string UserTypeNameEn { get; set; }
	public bool IsActive { get; set; }
	public DateTime? CreationDate { get; set; }
	public List<ApplicationPagesDto> ApplicationPages { get; set; }
}
