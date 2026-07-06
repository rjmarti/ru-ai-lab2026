using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class IndexModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly IDeleteAplicacionUseCase _delete;

    public IEnumerable<AplicacionResponse> Aplicaciones { get; private set; } = [];

    [TempData] public string? Mensaje { get; set; }
    [TempData] public string? Error   { get; set; }

    public IndexModel(IAdminQueryService query, IDeleteAplicacionUseCase delete)
    {
        _query  = query;
        _delete = delete;
    }

    public async Task OnGetAsync() =>
        Aplicaciones = await _query.GetAllAplicacionesAsync();

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        try
        {
            await _delete.ExecuteAsync(id);
            Mensaje = "Aplicación eliminada correctamente.";
        }
        catch (DomainException ex) { Error = ex.Message; }
        return RedirectToPage();
    }
}
