using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Usuarios;

public class IndexModel(IUsuarioService usuarioService) : PageModel
{
    public IEnumerable<UsuarioResponse> Usuarios { get; set; } = [];

    public async Task OnGetAsync()
    {
        Usuarios = await usuarioService.ListarAsync();
    }

    public async Task<IActionResult> OnPostBajaAsync(int id)
    {
        await usuarioService.DarDeBajaAsync(id);
        TempData["Mensaje"] = "Usuario dado de baja. Sus permisos activos fueron caducados.";
        return RedirectToPage();
    }
}
