using ReciclajeApp.Domain.Entities.Permissions;

namespace ReciclajeApp.Domain.Interfaces.Permissions;

public interface IPermissionRepository
{
    Task<IReadOnlyCollection<Permission>> GetAllAsync();
    Task<Permission?> GetByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<Permission> CreateAsync(Permission permission);
    Task UpdateAsync(Permission permission);
    Task DeleteAsync(Permission permission);
}
