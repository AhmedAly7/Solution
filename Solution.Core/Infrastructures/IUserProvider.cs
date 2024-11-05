namespace Solution.Core.Infrastructures;

public interface IUserProvider
{
	public int UserId { get; }
	public int CurrentBranchID { get; }
	public int CurrentOrganizationId { get; }
}