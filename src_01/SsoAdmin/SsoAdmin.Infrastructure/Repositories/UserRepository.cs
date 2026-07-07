using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Infrastructure.Data;

namespace SsoAdmin.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactory _factory;

    public UserRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Id = @Id", new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<User>("SELECT * FROM Users");
    }

    public async Task<int> CreateAsync(User user)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(
            "INSERT INTO Users (Name, IsActive) VALUES (@Name, @IsActive); SELECT last_insert_rowid();",
            user);
    }

    public async Task UpdateAsync(User user)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(
            "UPDATE Users SET Name = @Name, IsActive = @IsActive WHERE Id = @Id",
            user);
    }
}
