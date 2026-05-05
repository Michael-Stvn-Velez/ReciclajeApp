using ReciclajeApp.Domain.Entities.Roles;

namespace ReciclajeApp.Domain.Interfaces.Roles;

public interface IRoleRepository
{
    Task<IReadOnlyCollection<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<Role> CreateAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Role role);
}
