using Solution.Core.Enums;

namespace Solution.DTO.Models;

public class BaseSearchModel
{
	public BaseSearchModel()
	{
		SortField = "Id";
		SearchOrder = SearchOrdersEnum.Desc;
		PageIndex = 1;
		PageSize = 20;
	}

	public string SortField { get; set; }
	public SearchOrdersEnum SearchOrder { get; set; }
	public int PageIndex { get; set; }
	public int TotalRowsCount { get; set; }
	public int TotalPages { get; set; }
	public int PageSize { get; set; }
	public string SortBy { get { return SortField + " " + SearchOrder.ToString(); } }
}