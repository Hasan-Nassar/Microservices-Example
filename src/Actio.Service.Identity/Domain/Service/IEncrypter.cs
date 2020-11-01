namespace Actio.Service.Identity.Domain.Service
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}