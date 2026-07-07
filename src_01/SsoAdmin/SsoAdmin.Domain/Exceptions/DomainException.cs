namespace SsoAdmin.Domain.Exceptions;

/// <summary>Excepción base para violaciones de reglas de negocio (HTTP 400).</summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
