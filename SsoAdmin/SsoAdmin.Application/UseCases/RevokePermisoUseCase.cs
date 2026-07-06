using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para revocar un permiso de acceso.</summary>
public interface IRevokePermisoUseCase
{
    Task ExecuteAsync(int permisoId);
}

/// <summary>
/// Revoca el permiso estableciendo FechaHasta en la fecha actual (RF-05, AC-05).
/// </summary>
public class RevokePermisoUseCase : IRevokePermisoUseCase
{
    private readonly IPermissionRepository _permissions;

    public RevokePermisoUseCase(IPermissionRepository permissions) => _permissions = permissions;

    public async Task ExecuteAsync(int permisoId)
    {
        var permiso = await _permissions.GetByIdAsync(permisoId)
            ?? throw new NotFoundException($"Permiso {permisoId} no encontrado.");

        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        permiso.FechaHasta = today;
        await _permissions.UpdateAsync(permiso);
    }
}
