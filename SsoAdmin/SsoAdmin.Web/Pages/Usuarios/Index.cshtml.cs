using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly IDeactivateUsuarioUseCase _deactivate;

    public IEnumerable<UsuarioResponse> Usuarios { get; private set; } = [];

    [TempData] public string? Mensaje { get; set; }
    [TempData] public string? Error   { get; set; }

    public IndexModel(IAdminQueryService query, IDeactivateUsuarioUseCase deactivate)
    {
        _query      = query;
        _deactivate = deactivate;
    }

    public async Task OnGetAsync() =>
        Usuarios = await _query.GetAllUsuariosAsync();

    public async Task<IActionResult> OnPostBajaAsync(int id)
    {
        try
        {
            await _deactivate.ExecuteAsync(id);
            Mensaje = "Usuario dado de baja correctamente.";
        }
        catch (DomainException ex) { Error = ex.Message; }
        return RedirectToPage();
    }
}
