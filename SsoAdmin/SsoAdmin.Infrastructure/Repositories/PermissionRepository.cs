using Dapper;
using SsoAdmin.Application.Interfaces;
using SsoAdmin.Domain.Entities;
using SsoAdmin.Infrastructure.Data;

namespace SsoAdmin.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly DbConnectionFactory _factory;

    public PermissionRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Permission?> GetByIdAsync(int id)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Permission>(
            "SELECT * FROM Permissions WHERE Id = @Id", new { Id = id });
    }

    public async Task<Permission?> GetActivePermissionAsync(int userId, int applicationId, DateOnly today)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<Permission>(
            """
            SELECT * FROM Permissions
            WHERE UserId = @UserId
              AND ApplicationId = @ApplicationId
              AND FechaDesde <= @Today
              AND (FechaHasta IS NULL OR FechaHasta >= @Today)
            """,
            new { UserId = userId, ApplicationId = applicationId, Today = today.ToString("yyyy-MM-dd") });
    }

    public async Task<IEnumerable<Permission>> GetActiveByUserIdAsync(int userId, DateOnly today)
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Permission>(
            """
            SELECT * FROM Permissions
            WHERE UserId = @UserId
              AND (FechaHasta IS NULL OR FechaHasta >= @Today)
            """,
            new { UserId = userId, Today = today.ToString("yyyy-MM-dd") });
    }

    public async Task<IEnumerable<Permission>> GetByUserAndApplicationAsync(int userId, int applicationId)
    {
        using var conn = _factory.Create();
        return await conn.QueryAsync<Permission>(
            "SELECT * FROM Permissions WHERE UserId = @UserId AND ApplicationId = @ApplicationId",
            new { UserId = userId, ApplicationId = applicationId });
    }

    public async Task<int> CreateAsync(Permission permission)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(
            """
            INSERT INTO Permissions (UserId, ApplicationId, FechaDesde, FechaHasta)
            VALUES (@UserId, @ApplicationId, @FechaDesde, @FechaHasta);
            SELECT last_insert_rowid();
            """,
            new
            {
                permission.UserId,
                permission.ApplicationId,
                FechaDesde = permission.FechaDesde.ToString("yyyy-MM-dd"),
                FechaHasta = permission.FechaHasta?.ToString("yyyy-MM-dd")
            });
    }

    public async Task UpdateAsync(Permission permission)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(
            "UPDATE Permissions SET FechaHasta = @FechaHasta WHERE Id = @Id",
            new
            {
                FechaHasta = permission.FechaHasta?.ToString("yyyy-MM-dd"),
                permission.Id
            });
    }

    public async Task RevokeAllActiveAsync(int userId, DateOnly today)
    {
        using var conn = _factory.Create();
        await conn.ExecuteAsync(
            """
            UPDATE Permissions SET FechaHasta = @Today
            WHERE UserId = @UserId
              AND (FechaHasta IS NULL OR FechaHasta >= @Today)
            """,
            new { UserId = userId, Today = today.ToString("yyyy-MM-dd") });
    }
}
