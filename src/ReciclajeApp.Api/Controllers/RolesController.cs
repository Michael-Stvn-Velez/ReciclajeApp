using Microsoft.AspNetCore.Mvc;
using ReciclajeApp.Application.UseCases.Roles;
using ReciclajeApp.Domain.Request.Roles;

namespace ReciclajeApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RolesController : ControllerBase
{
    private readonly RoleUseCases _roleUseCases;

    public RolesController(RoleUseCases roleUseCases)
    {
        _roleUseCases = roleUseCases;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _roleUseCases.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _roleUseCases.GetByIdAsync(id);
        if (result is null) throw new KeyNotFoundException($"No existe un rol con id {id}.");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
    {
        var result = await _roleUseCases.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleRequest request)
    {
        if (id != request.Id)
        {
            throw new ArgumentException("El id de la ruta no coincide con el id del cuerpo.");
        }

        var result = await _roleUseCases.UpdateAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleUseCases.DeleteAsync(id);
        return NoContent();
    }
}
