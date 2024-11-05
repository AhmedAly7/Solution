using System.Security.Cryptography;
using System.Text;

namespace Solution.Core.Helpers;

public class Protector
{
	static byte[] s_additionalEntropy = { 2, 0, 2, 1, 5 };

	public Protector()
	{
	}

	public static byte[] Protect(byte[] data)
	{
		try
		{
			// Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
			// only by the same current user.
			return ProtectedData.Protect(data, s_additionalEntropy, DataProtectionScope.LocalMachine);
		}
		catch (CryptographicException e)
		{
			Console.WriteLine("Data was not encrypted. An error occurred.");
			Console.WriteLine(e.ToString());
			return null;
		}
	}

	public static byte[] Unprotect(byte[] data)
	{
		try
		{
			//Decrypt the data using DataProtectionScope.CurrentUser.
			return ProtectedData.Unprotect(data, s_additionalEntropy, DataProtectionScope.LocalMachine);
		}
		catch (CryptographicException e)
		{
			Console.WriteLine("Data was not decrypted. An error occurred.");
			Console.WriteLine(e.ToString());
			return null;
		}
	}

	public static void PrintValues(Byte[] myArr)
	{
		foreach (Byte i in myArr)
		{
			Console.Write("\t{0}", i);
		}
		Console.WriteLine();
	}

	public static string ComputeSha256Hash(string rawData)
	{

		// Create a SHA256   
		using (SHA256 sha256Hash = SHA256.Create())
		{
			// ComputeHash - returns byte array  
			byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
			//  return Convert.ToBase64String(bytes);
			// Convert byte array to a string   
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				builder.Append(bytes[i].ToString("x2"));
			}
			return builder.ToString();
		}
	}
}
