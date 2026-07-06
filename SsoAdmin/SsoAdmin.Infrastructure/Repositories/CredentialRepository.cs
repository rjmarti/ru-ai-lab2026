using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Infrastructure.Data;

namespace SsoAdmin.Infrastructure.Repositories;

public class CredentialRepository : ICredentialRepository
{
    private readonly DbConnectionFactory _factory;

    public CredentialRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Credential?> GetByUsernameAndEmisorAsync(string username, string emisor)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Credential>(
            "SELECT * FROM Credentials WHERE Username = @Username AND Emisor = @Emisor",
            new { Username = username, Emisor = emisor });
    }

    public async Task<IEnumerable<Credential>> GetAllAsync()
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Credential>("SELECT * FROM Credentials");
    }

    public async Task<IEnumerable<Credential>> GetByUserIdAsync(int userId)
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Credential>(
            "SELECT * FROM Credentials WHERE UserId = @UserId",
            new { UserId = userId });
    }

    public async Task<int> CreateAsync(Credential credential)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(
            """
            INSERT INTO Credentials (UserId, Username, Emisor)
            VALUES (@UserId, @Username, @Emisor);
            SELECT last_insert_rowid();
            """,
            credential);
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync("DELETE FROM Credentials WHERE Id = @Id", new { Id = id });
    }
}
