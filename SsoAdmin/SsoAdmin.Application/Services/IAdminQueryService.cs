using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Services;

/// <summary>Servicio de consultas de solo lectura para la interfaz de administración.</summary>
public interface IAdminQueryService
{
    Task<IEnumerable<UsuarioResponse>> GetAllUsuariosAsync();
    Task<UsuarioResponse?> GetUsuarioByIdAsync(int id);
    Task<IEnumerable<CredencialConUsuarioResponse>> GetAllCredencialesAsync();
    Task<IEnumerable<AplicacionResponse>> GetAllAplicacionesAsync();
    Task<AplicacionResponse?> GetAplicacionByIdAsync(int id);
}
