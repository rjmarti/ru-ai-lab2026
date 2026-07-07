using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;
using SsoAdmin.Models;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de administración de credenciales.</summary>
public class CredencialService(SsoAdminDbContext db, ILogger<CredencialService> logger) : ICredencialService
{
    public async Task<IEnumerable<CredencialResponse>> ListarAsync()
    {
        return await db.Credenciales
            .Include(c => c.Usuario)
            .OrderBy(c => c.Emisor).ThenBy(c => c.Username)
            .Select(c => MapToResponse(c))
            .ToListAsync();
    }

    /// <summary>Crea una credencial validando unicidad de (username, emisor) (RF-01, RF-02).</summary>
    public async Task<(CredencialResponse? credencial, string? error)> CrearAsync(CreateCredencialRequest request)
    {
        bool existe = await db.Credenciales.AnyAsync(c =>
            c.Username == request.Username && c.Emisor == request.Emisor);

        if (existe)
            return (null, "La combinación (username, emisor) ya existe en el sistema.");

        bool usuarioExiste = await db.Usuarios.AnyAsync(u => u.Id == request.UsuarioId);
        if (!usuarioExiste)
            return (null, "El usuario indicado no existe.");

        var credencial = new Credencial
        {
            Username = request.Username,
            Emisor = request.Emisor,
            UsuarioId = request.UsuarioId
        };

        db.Credenciales.Add(credencial);
        await db.SaveChangesAsync();

        await db.Entry(credencial).Reference(c => c.Usuario).LoadAsync();
        logger.LogInformation("Credencial creada: {Id} ({Username}/{Emisor})", credencial.Id, credencial.Username, credencial.Emisor);
        return (MapToResponse(credencial), null);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        Credencial? credencial = await db.Credenciales.FindAsync(id);
        if (credencial is null) return false;

        db.Credenciales.Remove(credencial);
        await db.SaveChangesAsync();
        logger.LogInformation("Credencial eliminada: {Id}", id);
        return true;
    }

    private static CredencialResponse MapToResponse(Credencial c) => new()
    {
        Id = c.Id,
        Username = c.Username,
        Emisor = c.Emisor,
        UsuarioId = c.UsuarioId,
        UsuarioNombre = c.Usuario?.Nombre ?? string.Empty
    };
}
