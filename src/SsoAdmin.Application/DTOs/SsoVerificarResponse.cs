namespace SsoAdmin.Application.DTOs;

public class SsoVerificarResponse
{
    public bool Allowed { get; set; }
    public string? Motivo { get; set; }

    public static SsoVerificarResponse Permitido() => new() { Allowed = true };
    public static SsoVerificarResponse Denegado(string motivo) => new() { Allowed = false, Motivo = motivo };
}
