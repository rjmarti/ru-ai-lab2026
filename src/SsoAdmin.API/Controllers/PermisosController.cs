using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/permisos")]
public class PermisosController(IPermisoService permisoService) : ControllerBase
{
    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<IActionResult> ListarPorUsuario(int usuarioId) =>
        Ok(await permisoService.ListarPorUsuarioAsync(usuarioId));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreatePermisoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        (PermisoResponse? permiso, string? error) = await permisoService.CrearAsync(request);

        if (error is not null)
            return Conflict(new { mensaje = error });

        return StatusCode(201, permiso);
    }

    [HttpPost("{id:int}/revocar")]
    public async Task<IActionResult> Revocar(int id)
    {
        (bool ok, string? error) = await permisoService.RevocarAsync(id);
        if (!ok) return NotFound(new { mensaje = error });
        return NoContent();
    }
}
