using ConsultaIbge.Application.Interfaces;
using BCrypt.Net;

namespace ConsultaIbge.Application.Services;

public sealed class PasswordHasherService : IPasswordHasherService
{
    private const int _workFactor = 12;
    private const HashType _hashType = HashType.SHA384;

    public string Hash(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password, _workFactor, _hashType);

    public bool Verify(string password, string hashedPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword, _hashType);
}
