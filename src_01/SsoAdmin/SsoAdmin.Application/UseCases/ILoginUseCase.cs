using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.UseCases;

/// <summary>Valida las credenciales de un usuario administrador.</summary>
public interface ILoginUseCase
{
    Task<bool> ExecuteAsync(LoginRequest request);
}
