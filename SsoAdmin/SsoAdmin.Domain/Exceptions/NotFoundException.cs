namespace SsoAdmin.Domain.Exceptions;

/// <summary>Recurso no encontrado (HTTP 404).</summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }
}
