namespace Solution.Core.Infrastructures;

public interface ISmsSenderService
{
	Task<string> SendSms(string message, params string[] mobileNumbers);
}