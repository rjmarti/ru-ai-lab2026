using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Administración de permisos de acceso de usuarios a aplicaciones.</summary>
public interface IPermisoService
{
    Task<IEnumerable<PermisoResponse>> ListarPorUsuarioAsync(int usuarioId);
    Task<(PermisoResponse? permiso, string? error)> CrearAsync(CreatePermisoRequest request);
    Task<(bool ok, string? error)> RevocarAsync(int id);
}
