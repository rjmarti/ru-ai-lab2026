using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Administración de aplicaciones registradas en el SSO.</summary>
public interface IAplicacionService
{
    Task<IEnumerable<AplicacionResponse>> ListarAsync();
    Task<AplicacionResponse?> ObtenerAsync(int id);
    Task<AplicacionResponse> CrearAsync(CreateAplicacionRequest request);
    Task<AplicacionResponse?> EditarAsync(int id, EditAplicacionRequest request);
    Task<bool> EliminarAsync(int id);
}
