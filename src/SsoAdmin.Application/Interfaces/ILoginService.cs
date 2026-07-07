namespace SsoAdmin.Application.Interfaces;

/// <summary>Autentica usuarios de SI contra la tabla de login local.</summary>
public interface ILoginService
{
    Task<bool> AutenticarAsync(string username, string password);
}
