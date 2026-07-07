using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para crear una credencial.</summary>
public interface ICreateCredencialUseCase
{
    Task<CredencialResponse> ExecuteAsync(CreateCredencialRequest request);
}

/// <summary>
/// Crea una credencial validando que la combinación (username, emisor) sea única (RF-01, AC-01, AC-02).
/// </summary>
public class CreateCredencialUseCase : ICreateCredencialUseCase
{
    private readonly ICredentialRepository _credentials;
    private readonly IUserRepository _users;

    public CreateCredencialUseCase(ICredentialRepository credentials, IUserRepository users)
    {
        _credentials = credentials;
        _users       = users;
    }

    public async Task<CredencialResponse> ExecuteAsync(CreateCredencialRequest request)
    {
        var usuarioExiste = await _users.GetByIdAsync(request.UserId);
        if (usuarioExiste is null)
            throw new NotFoundException($"Usuario {request.UserId} no encontrado.");

        var duplicada = await _credentials.GetByUsernameAndEmisorAsync(request.Username, request.Emisor);
        if (duplicada is not null)
            throw new DomainException(
                $"La combinación (username='{request.Username}', emisor='{request.Emisor}') ya existe.");

        Credential credencial = new()
        {
            UserId   = request.UserId,
            Username = request.Username,
            Emisor   = request.Emisor
        };
        credencial.Id = await _credentials.CreateAsync(credencial);

        return new CredencialResponse
        {
            Id       = credencial.Id,
            UserId   = credencial.UserId,
            Username = credencial.Username,
            Emisor   = credencial.Emisor
        };
    }
}
