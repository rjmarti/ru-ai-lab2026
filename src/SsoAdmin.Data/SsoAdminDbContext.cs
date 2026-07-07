using Microsoft.EntityFrameworkCore;
using SsoAdmin.Models;

namespace SsoAdmin.Data;

/// <summary>Contexto principal de base de datos para la aplicación SsoAdmin.</summary>
public class SsoAdminDbContext(DbContextOptions<SsoAdminDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Credencial> Credenciales => Set<Credencial>();
    public DbSet<Aplicacion> Aplicaciones => Set<Aplicacion>();
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<LoginUsuario> LoginUsuarios => Set<LoginUsuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credencial>(e =>
        {
            e.HasIndex(c => new { c.Username, c.Emisor }).IsUnique();
        });

        modelBuilder.Entity<Aplicacion>(e =>
        {
            e.Property(a => a.Url).IsRequired();
        });

    }
}
