using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class CreateModel : PageModel
{
    private readonly ICreateAplicacionUseCase _create;

    [BindProperty] public CreateAplicacionRequest Input { get; set; } = new();
    [TempData]     public string? Mensaje { get; set; }

    public CreateModel(ICreateAplicacionUseCase create) => _create = create;

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            await _create.ExecuteAsync(Input);
            Mensaje = $"Aplicación '{Input.Name}' registrada correctamente.";
            return RedirectToPage("Index");
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
