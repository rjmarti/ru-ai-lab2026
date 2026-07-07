using SsoAdmin.Application.DTOs;

namespace SsoAdmin.Application.UseCases;

public interface IVerificarAccesoUseCase
{
    Task<VerificarResponse> ExecuteAsync(VerificarRequest request);
}
