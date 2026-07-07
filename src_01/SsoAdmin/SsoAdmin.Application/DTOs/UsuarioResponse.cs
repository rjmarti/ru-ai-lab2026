namespace SsoAdmin.Application.DTOs;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
