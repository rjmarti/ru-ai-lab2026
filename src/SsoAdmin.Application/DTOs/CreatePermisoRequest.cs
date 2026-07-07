using System.ComponentModel.DataAnnotations;

namespace SsoAdmin.Application.DTOs;

public class CreatePermisoRequest
{
    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int AplicacionId { get; set; }

    [Required]
    public DateOnly FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }
}
