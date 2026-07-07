using Microsoft.AspNetCore.Mvc;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.UseCases;

namespace SsoAdmin.API.Controllers;

[ApiController]
[Route("api/sso")]
public class SsoController : ControllerBase
{
    private readonly IVerificarAccesoUseCase _useCase;
    private readonly ILogger<SsoController> _logger;

    public SsoController(IVerificarAccesoUseCase useCase, ILogger<SsoController> logger)
    {
        _useCase = useCase;
        _logger  = logger;
    }

    /// <summary>
    /// Verifica si una credencial tiene acceso a una aplicación.
    /// Devuelve 200 en todos los casos resueltos, incluso denegaciones.
    /// </summary>
    [HttpPost("verificar")]
    [ProducesResponseType(typeof(VerificarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Verificar([FromBody] VerificarRequest request)
    {
        try
        {
            VerificarResponse response = await _useCase.ExecuteAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al verificar acceso");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
