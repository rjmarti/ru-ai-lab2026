using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;
using SsoAdmin.Models;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de administración de aplicaciones.</summary>
public class AplicacionService(SsoAdminDbContext db, ILogger<AplicacionService> logger) : IAplicacionService
{
    public async Task<IEnumerable<AplicacionResponse>> ListarAsync()
    {
        return await db.Aplicaciones
            .OrderBy(a => a.Nombre)
            .Select(a => MapToResponse(a))
            .ToListAsync();
    }

    public async Task<AplicacionResponse?> ObtenerAsync(int id)
    {
        Aplicacion? app = await db.Aplicaciones.FindAsync(id);
        return app is null ? null : MapToResponse(app);
    }

    public async Task<AplicacionResponse> CrearAsync(CreateAplicacionRequest request)
    {
        var aplicacion = new Aplicacion { Nombre = request.Nombre, Url = request.Url };
        db.Aplicaciones.Add(aplicacion);
        await db.SaveChangesAsync();
        logger.LogInformation("Aplicación creada: {Id} - {Nombre}", aplicacion.Id, aplicacion.Nombre);
        return MapToResponse(aplicacion);
    }

    public async Task<AplicacionResponse?> EditarAsync(int id, EditAplicacionRequest request)
    {
        Aplicacion? aplicacion = await db.Aplicaciones.FindAsync(id);
        if (aplicacion is null) return null;

        aplicacion.Nombre = request.Nombre;
        aplicacion.Url = request.Url;
        await db.SaveChangesAsync();
        return MapToResponse(aplicacion);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Aplicacion? aplicacion = await db.Aplicaciones.FindAsync(id);
        if (aplicacion is null) return false;

        db.Aplicaciones.Remove(aplicacion);
        await db.SaveChangesAsync();
        logger.LogInformation("Aplicación eliminada: {Id}", id);
        return true;
    }

    private static AplicacionResponse MapToResponse(Aplicacion a) => new()
    {
        Id = a.Id,
        Nombre = a.Nombre,
        Url = a.Url
    };
}
