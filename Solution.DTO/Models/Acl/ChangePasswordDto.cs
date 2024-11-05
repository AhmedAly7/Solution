namespace Solution.DTO.Models.Acl;

public class ChangePasswordDto
{
	public Guid UserGuid { get; set; }
	public string OldPassword { get; set; }
	public string NewPassword { get; set; }
	public string NewPasswordConfirmation { get; set; }
}