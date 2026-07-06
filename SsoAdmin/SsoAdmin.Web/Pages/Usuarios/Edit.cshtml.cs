using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Usuarios;

public class EditModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly IEditUsuarioUseCase _edit;

    [BindProperty] public EditUsuarioRequest Input { get; set; } = new();
    [TempData]     public string? Mensaje { get; set; }

    public EditModel(IAdminQueryService query, IEditUsuarioUseCase edit)
    {
        _query = query;
        _edit  = edit;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        UsuarioResponse? usuario = await _query.GetUsuarioByIdAsync(id);
        if (usuario is null) return NotFound();
        Input = new EditUsuarioRequest { Id = usuario.Id, Name = usuario.Name };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        try
        {
            await _edit.ExecuteAsync(Input);
            Mensaje = "Usuario actualizado correctamente.";
            return RedirectToPage("Index");
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
