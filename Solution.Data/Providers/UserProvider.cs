using Microsoft.AspNetCore.Http;
using Solution.Core.Infrastructures;
using System.Security.Claims;

namespace Solution.Data.Providers;

public class UserProvider : IUserProvider
{
	private IHttpContextAccessor _httpContextAccessor;
	public UserProvider(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

	public int UserId
	{
		get
		{
			try
			{
				int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

				return userId;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}


	public int CurrentBranchID => 1;

	public int CurrentOrganizationId => 1;
}