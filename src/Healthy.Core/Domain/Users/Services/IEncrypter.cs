namespace Healthy.Core.Domain.Users.Services
{
    public interface IEncrypter
    {
        string GetRandomSecureKey();
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}