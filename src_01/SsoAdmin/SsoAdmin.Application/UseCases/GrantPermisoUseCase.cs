using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para otorgar un permiso de acceso.</summary>
public interface IGrantPermisoUseCase
{
    Task<PermisoResponse> ExecuteAsync(CreatePermisoRequest request);
}

/// <summary>
/// Otorga acceso validando que los períodos no se solapen para el mismo usuario y aplicación (RF-04, AC-04).
/// </summary>
public class GrantPermisoUseCase : IGrantPermisoUseCase
{
    private readonly IUserRepository _users;
    private readonly IApplicationRepository _aplicaciones;
    private readonly IPermissionRepository _permissions;

    public GrantPermisoUseCase(
        IUserRepository users,
        IApplicationRepository aplicaciones,
        IPermissionRepository permissions)
    {
        _users        = users;
        _aplicaciones = aplicaciones;
        _permissions  = permissions;
    }

    public async Task<PermisoResponse> ExecuteAsync(CreatePermisoRequest request)
    {
        if (await _users.GetByIdAsync(request.UserId) is null)
            throw new NotFoundException($"Usuario {request.UserId} no encontrado.");

        if (await _aplicaciones.GetByIdAsync(request.AplicacionId) is null)
            throw new NotFoundException($"Aplicación {request.AplicacionId} no encontrada.");

        if (request.FechaHasta.HasValue && request.FechaHasta <= request.FechaDesde)
            throw new DomainException("FechaHasta debe ser posterior a FechaDesde.");

        var existentes = await _permissions.GetByUserAndApplicationAsync(request.UserId, request.AplicacionId);
        DateOnly nuevoFin = request.FechaHasta ?? DateOnly.MaxValue;

        foreach (Permission existente in existentes)
        {
            DateOnly existenteFin = existente.FechaHasta ?? DateOnly.MaxValue;
            bool solapa = request.FechaDesde <= existenteFin && existente.FechaDesde <= nuevoFin;
            if (solapa)
                throw new ConflictException(
                    $"El período solapa con un permiso existente (id={existente.Id}, " +
                    $"desde={existente.FechaDesde:yyyy-MM-dd}, " +
                    $"hasta={existente.FechaHasta?.ToString("yyyy-MM-dd") ?? "indefinido"}).");
        }

        Permission permiso = new()
        {
            UserId        = request.UserId,
            ApplicationId = request.AplicacionId,
            FechaDesde    = request.FechaDesde,
            FechaHasta    = request.FechaHasta
        };
        permiso.Id = await _permissions.CreateAsync(permiso);

        return new PermisoResponse
        {
            Id          = permiso.Id,
            UserId      = permiso.UserId,
            AplicacionId = permiso.ApplicationId,
            FechaDesde  = permiso.FechaDesde,
            FechaHasta  = permiso.FechaHasta
        };
    }
}
