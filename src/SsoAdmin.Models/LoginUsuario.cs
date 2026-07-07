namespace SsoAdmin.Models;

/// <summary>Cuenta de acceso al panel de administración de SI.</summary>
public class LoginUsuario
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
