using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;
using SsoAdmin.Models;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de administración de permisos.</summary>
public class PermisoService(SsoAdminDbContext db, ILogger<PermisoService> logger) : IPermisoService
{
    public async Task<IEnumerable<PermisoResponse>> ListarPorUsuarioAsync(int usuarioId)
    {
        return await db.Permisos
            .Include(p => p.Usuario)
            .Include(p => p.Aplicacion)
            .Where(p => p.UsuarioId == usuarioId)
            .OrderBy(p => p.FechaDesde)
            .Select(p => MapToResponse(p))
            .ToListAsync();
    }

    /// <summary>Crea un permiso validando que los períodos no se solapen (RF-04).</summary>
    public async Task<(PermisoResponse? permiso, string? error)> CrearAsync(CreatePermisoRequest request)
    {
        if (request.FechaHasta.HasValue && request.FechaHasta.Value < request.FechaDesde)
            return (null, "La fecha_hasta no puede ser anterior a fecha_desde.");

        bool solapamiento = await db.Permisos.AnyAsync(p =>
            p.UsuarioId == request.UsuarioId &&
            p.AplicacionId == request.AplicacionId &&
            p.FechaDesde <= (request.FechaHasta ?? DateOnly.MaxValue) &&
            (p.FechaHasta == null || p.FechaHasta >= request.FechaDesde));

        if (solapamiento)
            return (null, "El período solapa con un permiso existente para este usuario y aplicación.");

        var permiso = new Permiso
        {
            UsuarioId = request.UsuarioId,
            AplicacionId = request.AplicacionId,
            FechaDesde = request.FechaDesde,
            FechaHasta = request.FechaHasta
        };

        db.Permisos.Add(permiso);
        await db.SaveChangesAsync();

        await db.Entry(permiso).Reference(p => p.Usuario).LoadAsync();
        await db.Entry(permiso).Reference(p => p.Aplicacion).LoadAsync();

        logger.LogInformation("Permiso creado: {Id} usuario {UsuarioId} app {AplicacionId}", permiso.Id, permiso.UsuarioId, permiso.AplicacionId);
        return (MapToResponse(permiso), null);
    }

    /// <summary>Revoca el permiso estableciendo fecha_hasta = hoy (RF-05).</summary>
    public async Task<(bool ok, string? error)> RevocarAsync(int id)
    {
        Permiso? permiso = await db.Permisos.FindAsync(id);
        if (permiso is null) return (false, "Permiso no encontrado.");

        DateOnly hoy = DateOnly.FromDateTime(DateTime.UtcNow);
        permiso.FechaHasta = hoy;
        await db.SaveChangesAsync();
        logger.LogInformation("Permiso {Id} revocado. FechaHasta={Fecha}", id, hoy);
        return (true, null);
    }

    private static PermisoResponse MapToResponse(Permiso p) => new()
    {
        Id = p.Id,
        UsuarioId = p.UsuarioId,
        UsuarioNombre = p.Usuario?.Nombre ?? string.Empty,
        AplicacionId = p.AplicacionId,
        AplicacionNombre = p.Aplicacion?.Nombre ?? string.Empty,
        FechaDesde = p.FechaDesde,
        FechaHasta = p.FechaHasta
    };
}
