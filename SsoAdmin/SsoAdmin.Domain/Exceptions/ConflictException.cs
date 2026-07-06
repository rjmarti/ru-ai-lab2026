namespace SsoAdmin.Domain.Exceptions;

/// <summary>Violación de regla de negocio por conflicto de datos (HTTP 409).</summary>
public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message) { }
}
