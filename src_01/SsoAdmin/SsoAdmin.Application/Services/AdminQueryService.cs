using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de consultas para la interfaz de administración.</summary>
public class AdminQueryService : IAdminQueryService
{
    private readonly IUserRepository _users;
    private readonly ICredentialRepository _credentials;
    private readonly IApplicationRepository _aplicaciones;

    public AdminQueryService(
        IUserRepository users,
        ICredentialRepository credentials,
        IApplicationRepository aplicaciones)
    {
        _users        = users;
        _credentials  = credentials;
        _aplicaciones = aplicaciones;
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAllUsuariosAsync()
    {
        IEnumerable<Domain.Entities.User> users = await _users.GetAllAsync();
        return users.Select(u => new UsuarioResponse { Id = u.Id, Name = u.Name, IsActive = u.IsActive });
    }

    public async Task<UsuarioResponse?> GetUsuarioByIdAsync(int id)
    {
        Domain.Entities.User? user = await _users.GetByIdAsync(id);
        if (user is null) return null;
        return new UsuarioResponse { Id = user.Id, Name = user.Name, IsActive = user.IsActive };
    }

    public async Task<IEnumerable<CredencialConUsuarioResponse>> GetAllCredencialesAsync()
    {
        IEnumerable<Domain.Entities.Credential> creds = await _credentials.GetAllAsync();
        IEnumerable<Domain.Entities.User> users        = await _users.GetAllAsync();
        Dictionary<int, string> mapaUsuarios = users.ToDictionary(u => u.Id, u => u.Name);

        return creds.Select(c => new CredencialConUsuarioResponse
        {
            Id            = c.Id,
            UserId        = c.UserId,
            UsuarioNombre = mapaUsuarios.TryGetValue(c.UserId, out string? name) ? name : "(desconocido)",
            Username      = c.Username,
            Emisor        = c.Emisor
        });
    }

    public async Task<IEnumerable<AplicacionResponse>> GetAllAplicacionesAsync()
    {
        IEnumerable<Domain.Entities.Aplicacion> apps = await _aplicaciones.GetAllAsync();
        return apps.Select(a => new AplicacionResponse { Id = a.Id, Name = a.Name, Url = a.Url });
    }

    public async Task<AplicacionResponse?> GetAplicacionByIdAsync(int id)
    {
        Domain.Entities.Aplicacion? app = await _aplicaciones.GetByIdAsync(id);
        if (app is null) return null;
        return new AplicacionResponse { Id = app.Id, Name = app.Name, Url = app.Url };
    }
}
