namespace Solution.Services.IServices.Infrastructure;

public interface IEncryptionProvider
{
	void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

	bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}