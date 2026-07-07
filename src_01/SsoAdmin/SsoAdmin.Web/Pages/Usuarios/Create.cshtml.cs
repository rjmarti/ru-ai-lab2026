using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Usuarios;

public class CreateModel : PageModel
{
    private readonly ICreateUsuarioUseCase _create;

    [BindProperty] public CreateUsuarioRequest Input { get; set; } = new();
    [TempData]     public string? Mensaje { get; set; }

    public CreateModel(ICreateUsuarioUseCase create) => _create = create;

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            await _create.ExecuteAsync(Input);
            Mensaje = $"Usuario '{Input.Name}' creado correctamente.";
            return RedirectToPage("Index");
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
