using System.Linq;
using ReciclajeApp.Domain.Entities.Roles;
using ReciclajeApp.Domain.Interfaces.Roles;
using ReciclajeApp.Domain.Request.Roles;
using ReciclajeApp.Domain.Response.Role;

namespace ReciclajeApp.Application.UseCases.Roles;

public sealed class RoleUseCases
{
    private readonly IRoleRepository _roleRepository;

    public RoleUseCases(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleResponse> CreateAsync(CreateRoleRequest request)
    {
        var normalizedName = NormalizeRoleName(request.Name);
        var normalizedDescription = request.Description.Trim();

        var exists = await _roleRepository.ExistsByNameAsync(normalizedName);
        if (exists)
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre '{normalizedName}'.");
        }

        var role = new Role
        {
            Name = normalizedName,
            Description = normalizedDescription
        };

        var createdRole = await _roleRepository.CreateAsync(role);

        return new RoleResponse
        {
            Id = createdRole.Id,
            Name = createdRole.Name,
            Description = createdRole.Description
        };
    }

    public async Task<RoleListResponse> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();

        var items = roles
            .Select(role => new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            })
            .ToArray();

        return new RoleListResponse { Items = items };
    }

    public async Task<RoleResponse?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role is null)
        {
            return null;
        }

        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }

    public async Task<RoleResponse> UpdateAsync(UpdateRoleRequest request)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"No existe un rol con id {request.Id}.");

        var normalizedName = NormalizeRoleName(request.Name);
        var normalizedDescription = request.Description.Trim();

        var roles = await _roleRepository.GetAllAsync();
        var duplicateNameExists = roles.Any(x =>
            x.Id != request.Id &&
            string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase));

        if (duplicateNameExists)
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre '{normalizedName}'.");
        }

        role.Name = normalizedName;
        role.Description = normalizedDescription;

        await _roleRepository.UpdateAsync(role);

        return new RoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No existe un rol con id {id}.");

        await _roleRepository.DeleteAsync(role);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var normalizedName = NormalizeRoleName(name);

        return await _roleRepository.ExistsByNameAsync(normalizedName);
    }

    private static string NormalizeRoleName(string name)
    {
        var normalizedName = name.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new ArgumentException("El nombre del rol es obligatorio.", nameof(name));
        }

        return normalizedName;
    }
}
