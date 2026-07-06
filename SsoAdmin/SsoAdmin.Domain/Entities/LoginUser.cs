namespace SsoAdmin.Domain.Entities;

/// <summary>Representa un usuario de administración con acceso al sistema.</summary>
public class LoginUser
{
    public int Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
}
