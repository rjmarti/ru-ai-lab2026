using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Administración de credenciales de usuarios.</summary>
public interface ICredencialService
{
    Task<IEnumerable<CredencialResponse>> ListarAsync();
    Task<(CredencialResponse? credencial, string? error)> CrearAsync(CreateCredencialRequest request);
    Task<bool> EliminarAsync(int id);
}
