namespace SsoAdmin.Models;

/// <summary>Permiso de acceso de un usuario a una aplicación durante un período.</summary>
public class Permiso
{
    public int Id { get; set; }
    public DateOnly FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int AplicacionId { get; set; }
    public Aplicacion Aplicacion { get; set; } = null!;
}
