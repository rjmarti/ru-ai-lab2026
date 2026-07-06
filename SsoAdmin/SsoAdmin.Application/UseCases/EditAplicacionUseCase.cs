using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para editar una aplicación.</summary>
public interface IEditAplicacionUseCase
{
    Task<AplicacionResponse> ExecuteAsync(EditAplicacionRequest request);
}

/// <summary>Edita nombre y URL de una aplicación existente (RF-11).</summary>
public class EditAplicacionUseCase : IEditAplicacionUseCase
{
    private readonly IApplicationRepository _aplicaciones;

    public EditAplicacionUseCase(IApplicationRepository aplicaciones) => _aplicaciones = aplicaciones;

    public async Task<AplicacionResponse> ExecuteAsync(EditAplicacionRequest request)
    {
        var aplicacion = await _aplicaciones.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Aplicación {request.Id} no encontrada.");

        if (string.IsNullOrWhiteSpace(request.Url))
            throw new DomainException("La URL de la aplicación no puede estar vacía.");

        aplicacion.Name = request.Name;
        aplicacion.Url  = request.Url;
        await _aplicaciones.UpdateAsync(aplicacion);

        return new AplicacionResponse { Id = aplicacion.Id, Name = aplicacion.Name, Url = aplicacion.Url };
    }
}
