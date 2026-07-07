namespace SsoAdmin.Application.DTOs;

public class CredencialResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Emisor { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
}
