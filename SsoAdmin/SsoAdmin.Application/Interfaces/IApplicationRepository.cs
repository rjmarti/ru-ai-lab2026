using SsoAdmin.Domain.Entities;

namespace SsoAdmin.Application.Interfaces;

public interface IApplicationRepository
{
    Task<Aplicacion?> GetByUrlAsync(string url);
    Task<Aplicacion?> GetByIdAsync(int id);
    Task<IEnumerable<Aplicacion>> GetAllAsync();
    Task<int> CreateAsync(Aplicacion aplicacion);
    Task UpdateAsync(Aplicacion aplicacion);
    Task DeleteAsync(int id);
}
