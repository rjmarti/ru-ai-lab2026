using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Usuarios;

public class EditarModel(IUsuarioService usuarioService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public EditUsuarioRequest Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        UsuarioResponse? usuario = await usuarioService.ObtenerAsync(Id);
        if (usuario is null) return NotFound();
        Input.Nombre = usuario.Nombre;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        UsuarioResponse? editado = await usuarioService.EditarAsync(Id, Input);
        if (editado is null) return NotFound();

        TempData["Mensaje"] = "Usuario actualizado correctamente.";
        return RedirectToPage("Index");
    }
}
