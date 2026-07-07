using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Credenciales;

public class IndexModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly IDeleteCredencialUseCase _delete;

    public IEnumerable<CredencialConUsuarioResponse> Credenciales { get; private set; } = [];

    [TempData] public string? Mensaje { get; set; }
    [TempData] public string? Error   { get; set; }

    public IndexModel(IAdminQueryService query, IDeleteCredencialUseCase delete)
    {
        _query  = query;
        _delete = delete;
    }

    public async Task OnGetAsync() =>
        Credenciales = await _query.GetAllCredencialesAsync();

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        try
        {
            await _delete.ExecuteAsync(id);
            Mensaje = "Credencial eliminada correctamente.";
        }
        catch (DomainException ex) { Error = ex.Message; }
        return RedirectToPage();
    }
}
