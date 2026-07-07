using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SsoAdmin.Application.DTOs;

public class VerificarRequest
{
    [Required]
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("emisor")]
    public string Emisor { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("aplicacion_url")]
    public string AplicacionUrl { get; set; } = string.Empty;
}
