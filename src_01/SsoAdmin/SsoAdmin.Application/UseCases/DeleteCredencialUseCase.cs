using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para eliminar una credencial.</summary>
public interface IDeleteCredencialUseCase
{
    Task ExecuteAsync(int id);
}

/// <summary>Elimina una credencial existente (RF-10).</summary>
public class DeleteCredencialUseCase : IDeleteCredencialUseCase
{
    private readonly ICredentialRepository _credentials;

    public DeleteCredencialUseCase(ICredentialRepository credentials) => _credentials = credentials;

    public async Task ExecuteAsync(int id) => await _credentials.DeleteAsync(id);
}
