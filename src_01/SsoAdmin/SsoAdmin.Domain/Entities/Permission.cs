namespace SsoAdmin.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ApplicationId { get; set; }
    public DateOnly FechaDesde { get; set; }
    public DateOnly? FechaHasta { get; set; }
}
