using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Credenciales;

public class IndexModel(ICredencialService credencialService) : PageModel
{
    public IEnumerable<CredencialResponse> Credenciales { get; set; } = [];

    public async Task OnGetAsync()
    {
        Credenciales = await credencialService.ListarAsync();
    }

    public async Task<IActionResult> OnPostEliminarAsync(int id)
    {
        await credencialService.EliminarAsync(id);
        TempData["Mensaje"] = "Credencial eliminada.";
        return RedirectToPage();
    }
}
