using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Aplicaciones;

public class EditModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly IEditAplicacionUseCase _edit;

    [BindProperty] public EditAplicacionRequest Input { get; set; } = new();
    [TempData]     public string? Mensaje { get; set; }

    public EditModel(IAdminQueryService query, IEditAplicacionUseCase edit)
    {
        _query = query;
        _edit  = edit;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        AplicacionResponse? app = await _query.GetAplicacionByIdAsync(id);
        if (app is null) return NotFound();
        Input = new EditAplicacionRequest { Id = app.Id, Name = app.Name, Url = app.Url };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            await _edit.ExecuteAsync(Input);
            Mensaje = "Aplicación actualizada correctamente.";
            return RedirectToPage("Index");
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
