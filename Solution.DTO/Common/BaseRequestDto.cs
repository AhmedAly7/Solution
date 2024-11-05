namespace Solution.DTO.Common;

public class BaseRequestDto<T>
{
	public Pagination Paging { get; set; }
	public T Data { get; set; }
}