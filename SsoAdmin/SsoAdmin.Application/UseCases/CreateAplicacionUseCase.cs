using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para registrar una aplicación.</summary>
public interface ICreateAplicacionUseCase
{
    Task<AplicacionResponse> ExecuteAsync(CreateAplicacionRequest request);
}

/// <summary>Registra una aplicación validando que la URL no esté vacía (RF-03, AC-03).</summary>
public class CreateAplicacionUseCase : ICreateAplicacionUseCase
{
    private readonly IApplicationRepository _aplicaciones;

    public CreateAplicacionUseCase(IApplicationRepository aplicaciones) => _aplicaciones = aplicaciones;

    public async Task<AplicacionResponse> ExecuteAsync(CreateAplicacionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Url))
            throw new DomainException("La URL de la aplicación no puede estar vacía.");

        Aplicacion aplicacion = new() { Name = request.Name, Url = request.Url };
        aplicacion.Id = await _aplicaciones.CreateAsync(aplicacion);

        return new AplicacionResponse { Id = aplicacion.Id, Name = aplicacion.Name, Url = aplicacion.Url };
    }
}
