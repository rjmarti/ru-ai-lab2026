using System.Text.Json.Serialization;

namespace SsoAdmin.Application.DTOs;

public class VerificarResponse
{
    [JsonPropertyName("allowed")]
    public bool Allowed { get; set; }

    [JsonPropertyName("motivo")]
    public string? Motivo { get; set; }

    public static VerificarResponse Permitido() =>
        new() { Allowed = true };

    public static VerificarResponse Denegado(string motivo) =>
        new() { Allowed = false, Motivo = motivo };
}
