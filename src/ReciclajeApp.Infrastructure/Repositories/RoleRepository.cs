using Microsoft.EntityFrameworkCore;
using ReciclajeApp.Domain.Entities.Roles;
using ReciclajeApp.Domain.Interfaces.Roles;
using ReciclajeApp.Infrastructure.Persistence;

namespace ReciclajeApp.Infrastructure.Repositories.Roles;

public sealed class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Role>> GetAllAsync()
    {
        return await _context.Set<Role>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToArrayAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Set<Role>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<Role>()
            .AnyAsync(x => x.Name == name);
    }

    public async Task<Role> CreateAsync(Role role)
    {
        await _context.Set<Role>().AddAsync(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task UpdateAsync(Role role)
    {
        _context.Set<Role>().Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role role)
    {
        _context.Set<Role>().Remove(role);
        await _context.SaveChangesAsync();
    }
}
