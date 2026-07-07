using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/sso")]
public class SsoController(ISsoService ssoService) : ControllerBase
{
    /// <summary>Verifica si una credencial tiene acceso a una aplicación.</summary>
    [HttpPost("verificar")]
    public async Task<IActionResult> Verificar([FromBody] SsoVerificarRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        SsoVerificarResponse response = await ssoService.VerificarAsync(request);
        return Ok(response);
    }
}
