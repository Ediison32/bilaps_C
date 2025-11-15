using webInmobiliary.Domain.Entities;

namespace webInmobiliary.Domain.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(int id);
    Task<IEnumerable<Admin>> GetAllAsync();
}