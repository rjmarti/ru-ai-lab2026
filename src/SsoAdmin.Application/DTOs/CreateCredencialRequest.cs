using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class CreateCredencialRequest
{
    [Required]
    public int UsuarioId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Emisor { get; set; } = string.Empty;
}
