using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class CreateUsuarioRequest
{
    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;
}
