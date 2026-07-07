using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;

namespace SsoAdmin.Application.Services;

/// <summary>Implementación del servicio de verificación SSO.</summary>
public class SsoService(SsoAdminDbContext db, ILogger<SsoService> logger) : ISsoService
{
    public async Task<SsoVerificarResponse> VerificarAsync(SsoVerificarRequest request)
    {
        var credencial = await db.Credenciales
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.Username == request.Username && c.Emisor == request.Emisor);

        if (credencial is null)
        {
            logger.LogInformation("Credencial no encontrada: {Username}/{Emisor}", request.Username, request.Emisor);
            return SsoVerificarResponse.Denegado("credencial_no_encontrada");
        }

        if (!credencial.Usuario.Activo)
        {
            logger.LogInformation("Usuario inactivo: {UsuarioId}", credencial.UsuarioId);
            return SsoVerificarResponse.Denegado("usuario_inactivo");
        }

        var aplicacion = await db.Aplicaciones
            .FirstOrDefaultAsync(a => a.Url == request.AplicacionUrl);

        if (aplicacion is null)
        {
            logger.LogInformation("Aplicación no encontrada: {Url}", request.AplicacionUrl);
            return SsoVerificarResponse.Denegado("aplicacion_no_encontrada");
        }

        var hoy = DateOnly.FromDateTime(DateTime.UtcNow);

        // Obtiene el permiso vigente más reciente para el usuario y la aplicación
        var permiso = await db.Permisos
            .Where(p =>
                p.UsuarioId == credencial.UsuarioId &&
                p.AplicacionId == aplicacion.Id &&
                p.FechaDesde <= hoy)
            .OrderByDescending(p => p.FechaDesde)
            .FirstOrDefaultAsync();

        if (permiso is null)
        {
            logger.LogInformation("Permiso no encontrado para usuario {UsuarioId} en app {AplicacionId}", credencial.UsuarioId, aplicacion.Id);
            return SsoVerificarResponse.Denegado("permiso_no_encontrado");
        }

        // FechaHasta es inclusiva para el otorgamiento pero al revocar se pone = hoy,
        // lo que debe denegar acceso inmediatamente (AC-05).
        if (permiso.FechaHasta.HasValue && permiso.FechaHasta.Value <= hoy)
        {
            logger.LogInformation("Permiso vencido para usuario {UsuarioId} en app {AplicacionId}", credencial.UsuarioId, aplicacion.Id);
            return SsoVerificarResponse.Denegado("permiso_vencido");
        }

        return SsoVerificarResponse.Permitido();
    }
}
