using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class EditarModel(IAplicacionService aplicacionService) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public EditAplicacionRequest Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        AplicacionResponse? app = await aplicacionService.ObtenerAsync(Id);
        if (app is null) return NotFound();
        Input.Nombre = app.Nombre;
        Input.Url = app.Url;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        AplicacionResponse? editada = await aplicacionService.EditarAsync(Id, Input);
        if (editada is null) return NotFound();

        TempData["Mensaje"] = "Aplicación actualizada correctamente.";
        return RedirectToPage("Index");
    }
}
