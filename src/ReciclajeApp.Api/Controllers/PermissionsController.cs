using Microsoft.AspNetCore.Mvc;
using ReciclajeApp.Application.UseCases.Permissions;
using ReciclajeApp.Domain.Request.Permissions;

namespace ReciclajeApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PermissionsController : ControllerBase
{
    private readonly PermissionUseCases _permissionUseCases;

    public PermissionsController(PermissionUseCases permissionUseCases)
    {
        _permissionUseCases = permissionUseCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _permissionUseCases.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _permissionUseCases.GetByIdAsync(id);
        if (result is null) throw new KeyNotFoundException($"No existe un permiso con id {id}.");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request)
    {
        var result = await _permissionUseCases.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePermissionRequest request)
    {
        if (id != request.Id)
        {
            throw new ArgumentException("El id de la ruta no coincide con el id del cuerpo.");
        }

        var result = await _permissionUseCases.UpdateAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _permissionUseCases.DeleteAsync(id);
        return NoContent();
    }
}
