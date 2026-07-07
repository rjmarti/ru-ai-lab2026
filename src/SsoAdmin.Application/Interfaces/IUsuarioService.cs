using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Administración de usuarios del sistema SSO.</summary>
public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponse>> ListarAsync();
    Task<UsuarioResponse?> ObtenerAsync(int id);
    Task<UsuarioResponse> CrearAsync(CreateUsuarioRequest request);
    Task<UsuarioResponse?> EditarAsync(int id, EditUsuarioRequest request);
    Task<bool> DarDeBajaAsync(int id);
}
