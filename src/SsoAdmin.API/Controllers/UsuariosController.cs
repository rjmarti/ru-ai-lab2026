using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar() =>
        Ok(await usuarioService.ListarAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Obtener(int id)
    {
        UsuarioResponse? usuario = await usuarioService.ObtenerAsync(id);
        return usuario is null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CreateUsuarioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        UsuarioResponse creado = await usuarioService.CrearAsync(request);
        return CreatedAtAction(nameof(Obtener), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, [FromBody] EditUsuarioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        UsuarioResponse? editado = await usuarioService.EditarAsync(id, request);
        return editado is null ? NotFound() : Ok(editado);
    }

    [HttpPost("{id:int}/baja")]
    public async Task<IActionResult> DarDeBaja(int id)
    {
        bool ok = await usuarioService.DarDeBajaAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
