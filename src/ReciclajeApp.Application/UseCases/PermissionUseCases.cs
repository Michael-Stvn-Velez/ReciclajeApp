using System.Linq;
using ReciclajeApp.Domain.Entities.Permissions;
using ReciclajeApp.Domain.Interfaces.Permissions;
using ReciclajeApp.Domain.Request.Permissions;
using ReciclajeApp.Domain.Response.Permission;

namespace ReciclajeApp.Application.UseCases.Permissions;

public sealed class PermissionUseCases
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionUseCases(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionResponse> CreateAsync(CreatePermissionRequest request)
    {
        var normalizedName = NormalizePermissionName(request.Name);
        var normalizedDescription = request.Description.Trim();

        var exists = await _permissionRepository.ExistsByNameAsync(normalizedName);
        if (exists)
        {
            throw new InvalidOperationException($"Ya existe un permiso con el nombre '{normalizedName}'.");
        }

        var permission = new Permission
        {
            Name = normalizedName,
            Description = normalizedDescription
        };

        var createdPermission = await _permissionRepository.CreateAsync(permission);

        return new PermissionResponse
        {
            Id = createdPermission.Id,
            Name = createdPermission.Name,
            Description = createdPermission.Description
        };
    }

    public async Task<PermissionListResponse> GetAllAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();

        var items = permissions
            .Select(permission => new PermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name,
                Description = permission.Description
            })
            .ToArray();

        return new PermissionListResponse { Items = items };
    }

    public async Task<PermissionResponse?> GetByIdAsync(int id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission is null)
        {
            return null;
        }

        return new PermissionResponse
        {
            Id = permission.Id,
            Name = permission.Name,
            Description = permission.Description
        };
    }

    public async Task<PermissionResponse> UpdateAsync(UpdatePermissionRequest request)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"No existe un permiso con id {request.Id}.");

        var normalizedName = NormalizePermissionName(request.Name);
        var normalizedDescription = request.Description.Trim();

        var permissions = await _permissionRepository.GetAllAsync();
        var duplicateNameExists = permissions.Any(x =>
            x.Id != request.Id &&
            string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase));

        if (duplicateNameExists)
        {
            throw new InvalidOperationException($"Ya existe un permiso con el nombre '{normalizedName}'.");
        }

        permission.Name = normalizedName;
        permission.Description = normalizedDescription;

        await _permissionRepository.UpdateAsync(permission);

        return new PermissionResponse
        {
            Id = permission.Id,
            Name = permission.Name,
            Description = permission.Description
        };
    }

    public async Task DeleteAsync(int id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"No existe un permiso con id {id}.");

        await _permissionRepository.DeleteAsync(permission);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        var normalizedName = NormalizePermissionName(name);

        return await _permissionRepository.ExistsByNameAsync(normalizedName);
    }

    private static string NormalizePermissionName(string name)
    {
        var normalizedName = name.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new ArgumentException("El nombre del permiso es obligatorio.", nameof(name));
        }

        return normalizedName;
    }

}
