using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.Services;
using SsoAdmin.Application.UseCases;
using SsoAdmin.Domain.Exceptions;

namespace SsoAdmin.Web.Pages.Credenciales;

public class CreateModel : PageModel
{
    private readonly IAdminQueryService _query;
    private readonly ICreateCredencialUseCase _create;

    [BindProperty] public CreateCredencialRequest Input { get; set; } = new();
    [TempData]     public string? Mensaje { get; set; }

    public IEnumerable<SelectListItem> UsuariosItems { get; private set; } = [];

    public CreateModel(IAdminQueryService query, ICreateCredencialUseCase create)
    {
        _query  = query;
        _create = create;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await CargarUsuariosAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CargarUsuariosAsync();
            return Page();
        }
        try
        {
            await _create.ExecuteAsync(Input);
            Mensaje = $"Credencial '{Input.Username}' creada correctamente.";
            return RedirectToPage("Index");
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await CargarUsuariosAsync();
            return Page();
        }
    }

    private async Task CargarUsuariosAsync()
    {
        IEnumerable<UsuarioResponse> usuarios = await _query.GetAllUsuariosAsync();
        UsuariosItems = usuarios
            .Where(u => u.IsActive)
            .Select(u => new SelectListItem(u.Name, u.Id.ToString()));
    }
}
