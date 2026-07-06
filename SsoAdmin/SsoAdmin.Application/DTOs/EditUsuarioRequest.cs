using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class EditUsuarioRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
