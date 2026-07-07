using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class CreateAplicacionRequest
{
    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;
}
