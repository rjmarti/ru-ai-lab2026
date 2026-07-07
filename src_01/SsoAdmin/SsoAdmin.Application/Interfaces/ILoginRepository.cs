using SsoAdmin.Domain.Entities;

namespace SsoAdmin.Application.Interfaces;

/// <summary>Acceso a datos de usuarios de administración.</summary>
public interface ILoginRepository
{
    Task<LoginUser?> FindByUsernameAsync(string username);
}
