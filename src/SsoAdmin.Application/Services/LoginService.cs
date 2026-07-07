using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Data;

namespace SsoAdmin.Application.Services;

/// <summary>Autentica usuarios de SI verificando el hash BCrypt almacenado.</summary>
public class LoginService(SsoAdminDbContext db, ILogger<LoginService> logger) : ILoginService
{
    public async Task<bool> AutenticarAsync(string username, string password)
    {
        var loginUsuario = await db.LoginUsuarios
            .FirstOrDefaultAsync(l => l.Username == username);

        if (loginUsuario is null)
        {
            logger.LogWarning("Intento de login con usuario inexistente: {Username}", username);
            return false;
        }

        bool valido = BCrypt.Net.BCrypt.Verify(password, loginUsuario.PasswordHash);

        if (!valido)
            logger.LogWarning("Password incorrecto para usuario: {Username}", username);

        return valido;
    }
}
