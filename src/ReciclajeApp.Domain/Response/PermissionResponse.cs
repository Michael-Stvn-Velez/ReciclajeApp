namespace ReciclajeApp.Domain.Response.Permission;

public sealed class PermissionResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
