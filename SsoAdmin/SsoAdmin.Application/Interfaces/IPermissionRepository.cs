using SsoAdmin.Domain.Entities;

namespace SsoAdmin.Application.Interfaces;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(int id);
    Task<Permission?> GetActivePermissionAsync(int userId, int applicationId, DateOnly today);
    Task<IEnumerable<Permission>> GetActiveByUserIdAsync(int userId, DateOnly today);
    Task<IEnumerable<Permission>> GetByUserAndApplicationAsync(int userId, int applicationId);
    Task<int> CreateAsync(Permission permission);
    Task UpdateAsync(Permission permission);
    Task RevokeAllActiveAsync(int userId, DateOnly today);
}
