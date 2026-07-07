using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Verifica si una credencial tiene acceso a una aplicación.</summary>
public interface ISsoService
{
    Task<SsoVerificarResponse> VerificarAsync(SsoVerificarRequest request);
}
