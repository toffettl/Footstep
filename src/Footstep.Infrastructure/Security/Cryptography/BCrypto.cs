namespace Footstep.Infrastructure.Security.Cryptography;

using Footstep.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;
internal class BCrypto : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash)
    {
        return BC.Verify(password, passwordHash);
    }
}
