using System.Text;

namespace Solution.Core.Helpers;

public class GenerateRandomPassword
{
	// Generate a random string with a given size and case.   
	// If second parameter is true, the return string is lowercase  
	public static string RandomPassword(int size, bool lowerCase)
	{
		StringBuilder builder = new StringBuilder();
		Random random = new Random();
		char ch;
		for (int i = 0; i < size; i++)
		{
			ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
			builder.Append(ch);
		}
		if (lowerCase)
			return builder.ToString().ToLower();
		return builder.ToString();
	}
}