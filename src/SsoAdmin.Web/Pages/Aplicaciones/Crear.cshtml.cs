using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class CrearModel(IAplicacionService aplicacionService) : PageModel
{
    [BindProperty]
    public CreateAplicacionRequest Input { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await aplicacionService.CrearAsync(Input);
        TempData["Mensaje"] = "Aplicación creada correctamente.";
        return RedirectToPage("Index");
    }
}
