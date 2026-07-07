using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/credenciales")]
public class CredencialesController(ICredencialService credencialService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar() =>
        Ok(await credencialService.ListarAsync());

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateCredencialRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        (CredencialResponse? credencial, string? error) = await credencialService.CrearAsync(request);

        if (error is not null)
            return BadRequest(new { mensaje = error });

        return StatusCode(201, credencial);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool ok = await credencialService.EliminarAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
