using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class CreateUsuarioRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
