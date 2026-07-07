using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Credenciales;

public class CrearModel(ICredencialService credencialService, IUsuarioService usuarioService) : PageModel
{
    [BindProperty]
    public CreateCredencialRequest Input { get; set; } = new();

    public List<SelectListItem> UsuariosItems { get; set; } = [];
    public string? ErrorMessage { get; set; }

    public async Task OnGetAsync()
    {
        await CargarUsuariosAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CargarUsuariosAsync();
            return Page();
        }

        (CredencialResponse? credencial, string? error) = await credencialService.CrearAsync(Input);

        if (error is not null)
        {
            ErrorMessage = error;
            await CargarUsuariosAsync();
            return Page();
        }

        TempData["Mensaje"] = "Credencial creada correctamente.";
        return RedirectToPage("Index");
    }

    private async Task CargarUsuariosAsync()
    {
        IEnumerable<UsuarioResponse> usuarios = await usuarioService.ListarAsync();
        UsuariosItems = usuarios
            .Where(u => u.Activo)
            .Select(u => new SelectListItem(u.Nombre, u.Id.ToString()))
            .ToList();
    }
}
