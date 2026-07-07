using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class EditUsuarioRequest
{
    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;
}
