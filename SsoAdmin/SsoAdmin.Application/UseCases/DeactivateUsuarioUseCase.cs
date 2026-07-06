using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para dar de baja lógica a un usuario.</summary>
public interface IDeactivateUsuarioUseCase
{
    Task ExecuteAsync(int userId);
}

/// <summary>
/// Da de baja lógica al usuario y caduca en cascada todos sus permisos activos (RF-06, AC-06).
/// </summary>
public class DeactivateUsuarioUseCase : IDeactivateUsuarioUseCase
{
    private readonly IUserRepository _users;
    private readonly IPermissionRepository _permissions;

    public DeactivateUsuarioUseCase(IUserRepository users, IPermissionRepository permissions)
    {
        _users       = users;
        _permissions = permissions;
    }

    public async Task ExecuteAsync(int userId)
    {
        var user = await _users.GetByIdAsync(userId)
            ?? throw new NotFoundException($"Usuario {userId} no encontrado.");

        if (!user.IsActive)
            return;

        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        user.IsActive = false;
        await _users.UpdateAsync(user);
        await _permissions.RevokeAllActiveAsync(userId, today);
    }
}
