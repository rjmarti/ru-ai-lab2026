using System.Security.Cryptography;
using System.Text;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Application.UseCases;

/// <summary>Autentica a un usuario de SI comparando el hash SHA-256 de la contraseña.</summary>
public class LoginUseCase : ILoginUseCase
{
    private readonly ILoginRepository _loginRepository;

    public LoginUseCase(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<bool> ExecuteAsync(LoginRequest request)
    {
        var user = await _loginRepository.FindByUsernameAsync(request.Username);
        if (user is null) return false;

        string hash = ComputeHash(request.Password);
        return string.Equals(user.PasswordHash, hash, StringComparison.OrdinalIgnoreCase);
    }

    private static string ComputeHash(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
