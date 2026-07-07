using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Infrastructure.Data;

namespace SsoAdmin.Infrastructure.Repositories;

/// <summary>Repositorio de usuarios de administración (tabla Login).</summary>
public class LoginRepository : ILoginRepository
{
    private readonly DbConnectionFactory _factory;

    public LoginRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<LoginUser?> FindByUsernameAsync(string username)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<LoginUser>(
            "SELECT Id, Username, PasswordHash FROM Login WHERE Username = @Username",
            new { Username = username });
    }
}
