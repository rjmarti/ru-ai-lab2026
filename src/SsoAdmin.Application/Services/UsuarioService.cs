using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;
using SsoAdmin.Models;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de administración de usuarios.</summary>
public class UsuarioService(SsoAdminDbContext db, ILogger<UsuarioService> logger) : IUsuarioService
{
    public async Task<IEnumerable<UsuarioResponse>> ListarAsync()
    {
        return await db.Usuarios
            .OrderBy(u => u.Nombre)
            .Select(u => MapToResponse(u))
            .ToListAsync();
    }

    public async Task<UsuarioResponse?> ObtenerAsync(int id)
    {
        Usuario? usuario = await db.Usuarios.FindAsync(id);
        return usuario is null ? null : MapToResponse(usuario);
    }

    public async Task<UsuarioResponse> CrearAsync(CreateUsuarioRequest request)
    {
        var usuario = new Usuario { Nombre = request.Nombre };
        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();
        logger.LogInformation("Usuario creado: {Id} - {Nombre}", usuario.Id, usuario.Nombre);
        return MapToResponse(usuario);
    }

    public async Task<UsuarioResponse?> EditarAsync(int id, EditUsuarioRequest request)
    {
        Usuario? usuario = await db.Usuarios.FindAsync(id);
        if (usuario is null) return null;

        usuario.Nombre = request.Nombre;
        await db.SaveChangesAsync();
        return MapToResponse(usuario);
    }

    /// <summary>Da de baja lógica al usuario y caduca todos sus permisos activos (RF-06).</summary>
    public async Task<bool> DarDeBajaAsync(int id)
    {
        Usuario? usuario = await db.Usuarios
            .Include(u => u.Permisos)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null) return false;

        DateOnly hoy = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (Permiso permiso in usuario.Permisos)
        {
            if (!permiso.FechaHasta.HasValue || permiso.FechaHasta.Value > hoy)
                permiso.FechaHasta = hoy;
        }

        usuario.Activo = false;
        await db.SaveChangesAsync();
        logger.LogInformation("Usuario dado de baja: {Id}. Permisos caducados: {Count}", id, usuario.Permisos.Count);
        return true;
    }

    private static UsuarioResponse MapToResponse(Usuario u) => new()
    {
        Id = u.Id,
        Nombre = u.Nombre,
        Activo = u.Activo,
        FechaCreacion = u.FechaCreacion
    };
}
