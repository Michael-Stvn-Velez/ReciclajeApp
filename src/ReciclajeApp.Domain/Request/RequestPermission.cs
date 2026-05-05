using ReciclajeApp.Domain.Response.Permission;

namespace ReciclajeApp.Domain.Request.Permissions;

public sealed class CreatePermissionRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class PermissionListResponse
{
    public IReadOnlyCollection<PermissionResponse> Items { get; init; } = Array.Empty<PermissionResponse>();
}

public sealed class UpdatePermissionRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}