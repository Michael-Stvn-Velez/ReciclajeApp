using Microsoft.EntityFrameworkCore;
using ReciclajeApp.Domain.Entities.Permissions;
using ReciclajeApp.Domain.Interfaces.Permissions;
using ReciclajeApp.Infrastructure.Persistence;

namespace ReciclajeApp.Infrastructure.Repositories.Permissions;

public sealed class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Permission>> GetAllAsync()
    {
        return await _context.Set<Permission>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToArrayAsync();
    }

    public async Task<Permission?> GetByIdAsync(int id)
    {
        return await _context.Set<Permission>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Set<Permission>()
            .AnyAsync(x => x.Name == name);
    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        await _context.Set<Permission>().AddAsync(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    public async Task UpdateAsync(Permission permission)
    {
        _context.Set<Permission>().Update(permission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission permission)
    {
        _context.Set<Permission>().Remove(permission);
        await _context.SaveChangesAsync();
    }
}
