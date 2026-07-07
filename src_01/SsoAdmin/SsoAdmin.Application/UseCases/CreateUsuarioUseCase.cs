using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;

namespace SsoAdmin.Application.UseCases;

/// <summary>Contrato para crear un usuario.</summary>
public interface ICreateUsuarioUseCase
{
    Task<UsuarioResponse> ExecuteAsync(CreateUsuarioRequest request);
}

/// <summary>Crea un nuevo usuario activo.</summary>
public class CreateUsuarioUseCase : ICreateUsuarioUseCase
{
    private readonly IUserRepository _users;

    public CreateUsuarioUseCase(IUserRepository users) => _users = users;

    public async Task<UsuarioResponse> ExecuteAsync(CreateUsuarioRequest request)
    {
        User user = new() { Name = request.Name, IsActive = true };
        user.Id = await _users.CreateAsync(user);
        return new UsuarioResponse { Id = user.Id, Name = user.Name, IsActive = user.IsActive };
    }
}
