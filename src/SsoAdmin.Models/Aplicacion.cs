namespace SsoAdmin.Models;

/// <summary>Aplicación registrada en el sistema SSO.</summary>
public class Aplicacion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
