using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para eliminar una aplicación.</summary>
public interface IDeleteAplicacionUseCase
{
    Task ExecuteAsync(int id);
}

/// <summary>Elimina una aplicación existente (RF-11).</summary>
public class DeleteAplicacionUseCase : IDeleteAplicacionUseCase
{
    private readonly IApplicationRepository _aplicaciones;

    public DeleteAplicacionUseCase(IApplicationRepository aplicaciones) => _aplicaciones = aplicaciones;

    public async Task ExecuteAsync(int id) => await _aplicaciones.DeleteAsync(id);
}
