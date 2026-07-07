namespace SsoAdmin.Application.DTOs;

public class PermisoResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AplicacionId { get; set; }
    public DateOnly FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
}
