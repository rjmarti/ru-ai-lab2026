using SsoAdmin.Domain.Entities;

namespace SsoAdmin.Application.Interfaces;

public interface ICredentialRepository
{
    Task<Credential?> GetByUsernameAndEmisorAsync(string username, string emisor);
    Task<IEnumerable<Credential>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Credential>> GetAllAsync();
    Task<int> CreateAsync(Credential credential);
    Task DeleteAsync(int id);
}
