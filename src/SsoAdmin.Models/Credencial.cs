namespace SsoAdmin.Models;

/// <summary>Credencial de un usuario en un emisor externo (ej. Google, Azure AD).</summary>
public class Credencial
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Emisor { get; set; } = string.Empty;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
