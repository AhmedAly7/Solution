using Solution.Services.IServices.Infrastructure;
using System.Security.Cryptography;

namespace Solution.Services.Services.Infrastructure;

public class EncryptionProvider : IEncryptionProvider
{
	public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
	{
		if (password == null) throw new ArgumentNullException("password");
		if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password is null or empty", "password");

		using (var hmac = new HMACSHA512())
		{
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}
	}

	public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
	{
		if (password == null) throw new ArgumentNullException("password");
		if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("password is null or empty", "password");
		if (storedHash != null && storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
		if (storedSalt != null && storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

		if (storedSalt != null)
		{
			using (var hmac = new HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
					if (computedHash[i] != storedHash[i]) return false;
			}
		}
		return true;
	}
}