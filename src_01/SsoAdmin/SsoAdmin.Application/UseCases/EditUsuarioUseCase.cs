using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para editar el nombre de un usuario.</summary>
public interface IEditUsuarioUseCase
{
    Task<UsuarioResponse> ExecuteAsync(EditUsuarioRequest request);
}

/// <summary>Edita el nombre de un usuario existente (RF-09).</summary>
public class EditUsuarioUseCase : IEditUsuarioUseCase
{
    private readonly IUserRepository _users;

    public EditUsuarioUseCase(IUserRepository users) => _users = users;

    public async Task<UsuarioResponse> ExecuteAsync(EditUsuarioRequest request)
    {
        var user = await _users.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Usuario {request.Id} no encontrado.");

        user.Name = request.Name;
        await _users.UpdateAsync(user);

        return new UsuarioResponse { Id = user.Id, Name = user.Name, IsActive = user.IsActive };
    }
}
