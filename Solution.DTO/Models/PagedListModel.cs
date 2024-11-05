namespace Solution.DTO.Models;

public class PagedListModel<T> where T : class
{
	#region ctor

	public PagedListModel(int currentPage, int pageSize)
	{
		QueryOptions = new BaseSearchModel()
		{
			PageIndex = currentPage,
			PageSize = pageSize
		};

		DataList = new List<T>();
	}

	public PagedListModel()
	{
		QueryOptions = new BaseSearchModel();
		DataList = new List<T>();
	}

	#endregion ctor

	public BaseSearchModel QueryOptions { get; set; }
	public IEnumerable<T> DataList { get; set; }
}