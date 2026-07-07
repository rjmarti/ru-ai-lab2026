namespace SsoAdmin.Application.DTOs;

public class CredencialConUsuarioResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Emisor { get; set; } = string.Empty;
}
