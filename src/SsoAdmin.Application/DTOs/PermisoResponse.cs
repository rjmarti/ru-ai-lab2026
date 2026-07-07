namespace SsoAdmin.Application.DTOs;

public class PermisoResponse
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNombre { get; set; } = string.Empty;
    public int AplicacionId { get; set; }
    public string AplicacionNombre { get; set; } = string.Empty;
    public DateOnly FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
}
