namespace SsoAdmin.Models;

/// <summary>Representa un usuario administrado por el sistema.</summary>
public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public ICollection<Credencial> Credenciales { get; set; } = new List<Credencial>();
    public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
