using Solution.Core.Enums;

namespace Solution.DTO.Common;

public class Pagination
{
	public string SortField { get; set; } = "Id";
	public SearchOrdersEnum SortDirection { get; set; } = SearchOrdersEnum.Desc;
	public int PageIndex { get; set; } = 0;
	public int PageSize { get; set; } = 1000;
	public string SortBy { get { return SortField + " " + SortDirection.ToString(); } }
}