using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Infrastructure.Data;

namespace SsoAdmin.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly DbConnectionFactory _factory;

    public ApplicationRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Aplicacion?> GetByUrlAsync(string url)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Aplicacion>(
            "SELECT * FROM Applications WHERE Url = @Url", new { Url = url });
    }

    public async Task<Aplicacion?> GetByIdAsync(int id)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Aplicacion>(
            "SELECT * FROM Applications WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<Aplicacion>> GetAllAsync()
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Aplicacion>("SELECT * FROM Applications");
    }

    public async Task<int> CreateAsync(Aplicacion aplicacion)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(
            """
            INSERT INTO Applications (Name, Url) VALUES (@Name, @Url);
            SELECT last_insert_rowid();
            """,
            aplicacion);
    }

    public async Task UpdateAsync(Aplicacion aplicacion)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(
            "UPDATE Applications SET Name = @Name, Url = @Url WHERE Id = @Id",
            aplicacion);
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync("DELETE FROM Applications WHERE Id = @Id", new { Id = id });
    }
}
