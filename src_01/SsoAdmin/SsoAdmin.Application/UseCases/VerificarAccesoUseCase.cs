using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Application.UseCases;

/// <summary>
/// Verifica si una credencial tiene acceso activo a una aplicación.
/// Implementa la cadena definida en RF-08: credencial → usuario → aplicación → permiso vigente.
/// </summary>
public class VerificarAccesoUseCase : IVerificarAccesoUseCase
{
    private readonly ICredentialRepository _credentials;
    private readonly IUserRepository _users;
    private readonly IApplicationRepository _applications;
    private readonly IPermissionRepository _permissions;

    public VerificarAccesoUseCase(
        ICredentialRepository credentials,
        IUserRepository users,
        IApplicationRepository applications,
        IPermissionRepository permissions)
    {
        _credentials  = credentials;
        _users        = users;
        _applications = applications;
        _permissions  = permissions;
    }

    public async Task<VerificarResponse> ExecuteAsync(VerificarRequest request)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        var credencial = await _credentials.GetByUsernameAndEmisorAsync(request.Username, request.Emisor);
        if (credencial is null)
            return VerificarResponse.Denegado(Motivos.CredencialNoEncontrada);

        var usuario = await _users.GetByIdAsync(credencial.UserId);
        if (usuario is null || !usuario.IsActive)
            return VerificarResponse.Denegado(Motivos.UsuarioInactivo);

        var aplicacion = await _applications.GetByUrlAsync(request.AplicacionUrl);
        if (aplicacion is null)
            return VerificarResponse.Denegado(Motivos.AplicacionNoEncontrada);

        var todosLosPermisos = await _permissions.GetByUserAndApplicationAsync(usuario.Id, aplicacion.Id);
        if (!todosLosPermisos.Any())
            return VerificarResponse.Denegado(Motivos.PermisoNoEncontrado);

        var permisoActivo = await _permissions.GetActivePermissionAsync(usuario.Id, aplicacion.Id, today);
        if (permisoActivo is null)
            return VerificarResponse.Denegado(Motivos.PermisoVencido);

        return VerificarResponse.Permitido();
    }
}
