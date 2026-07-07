using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/aplicaciones")]
public class AplicacionesController(IAplicacionService aplicacionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar() =>
        Ok(await aplicacionService.ListarAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        AplicacionResponse? app = await aplicacionService.ObtenerAsync(id);
        return app is null ? NotFound() : Ok(app);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateAplicacionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        AplicacionResponse creada = await aplicacionService.CrearAsync(request);
        return CreatedAtAction(nameof(Obtener), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, [FromBody] EditAplicacionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        AplicacionResponse? editada = await aplicacionService.EditarAsync(id, request);
        return editada is null ? NotFound() : Ok(editada);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool ok = await aplicacionService.EliminarAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
