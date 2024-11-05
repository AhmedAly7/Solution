namespace Solution.DTO.Common;

public class BaseResponseDto<T>
{

	public bool IsSuccess { get; private set; }
	public T Data { get; private set; }
	public string Message { get; set; }
	public string ReferenceNo { get; private set; }
	public List<string> Errors { get; private set; }
	public Dictionary<string, List<string>> PropErrors { get; private set; }

	public BaseResponseDto()
	{
		SetError("");
	}
	public void SetSuccess(T data, string message = default)
	{
		IsSuccess = true;
		Message = message;
		Data = data;
		ReferenceNo = default;
		Errors = default;
		PropErrors = default;
	}

	public void SetError(string message, string referenceNo = default, List<string> errors = null, Dictionary<string, List<string>> propErrors = null)
	{
		IsSuccess = false;
		Message += Message == null || Message.Length == 0 ? message : " , " + message;
		ReferenceNo = referenceNo;
		Errors = errors;
		PropErrors = propErrors;
		Data = default;
	}
}