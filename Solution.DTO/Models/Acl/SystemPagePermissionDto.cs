namespace Solution.DTO.Models.Acl;

public class SystemPagePermissionDto
{
	public int Id { get; set; }
	public int PermissionTermId { get; set; }
	public string PermissionTermNameAr { get; set; }
	public string PermissionTermNameEn { get; set; }
	public bool Included { get; set; }
}
