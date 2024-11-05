namespace Solution.DTO.Models.Acl;

public class ApplicationPagesDto
{
	public int AppId { get; set; }
	public string AppNameAr { get; set; }
	public string AppNameEn { get; set; }
	public List<SystemPageFormDto> SystemPages { get; set; }
}