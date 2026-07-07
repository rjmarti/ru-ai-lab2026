using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class EditAplicacionRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Url { get; set; } = string.Empty;
}
