using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class SsoVerificarRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Emisor { get; set; } = string.Empty;
    [Required]
    public string AplicacionUrl { get; set; } = string.Empty;
}
