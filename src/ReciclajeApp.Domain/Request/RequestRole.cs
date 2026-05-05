using ReciclajeApp.Domain.Response.Role;

namespace ReciclajeApp.Domain.Request.Roles;

public sealed class CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public sealed class RoleListResponse
{
    public IReadOnlyCollection<RoleResponse> Items { get; init; } = Array.Empty<RoleResponse>();
}

public sealed class UpdateRoleRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
