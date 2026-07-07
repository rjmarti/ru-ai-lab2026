using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class IndexModel(IAplicacionService aplicacionService) : PageModel
{
    public IEnumerable<AplicacionResponse> Aplicaciones { get; set; } = [];

    public async Task OnGetAsync()
    {
        Aplicaciones = await aplicacionService.ListarAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await aplicacionService.EliminarAsync(id);
        TempData["Mensaje"] = "Aplicación eliminada.";
        return RedirectToPage();
    }
}
