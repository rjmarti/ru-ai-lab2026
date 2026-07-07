using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Usuarios;

public class CrearModel(IUsuarioService usuarioService) : PageModel
{
    [BindProperty]
    public CreateUsuarioRequest Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await usuarioService.CrearAsync(Input);
        TempData["Mensaje"] = "Usuario creado correctamente.";
        return RedirectToPage("Index");
    }
}
