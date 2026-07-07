using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.Interfaces;

namespace SsoAdmin.Web.Pages;

public class LoginModel(ILoginService loginService) : PageModel
{
    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/Usuarios/Index");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        bool valido = await loginService.AutenticarAsync(Username, Password);
        if (!valido)
        {
            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }

        List<Claim> claims = [new Claim(ClaimTypes.Name, Username)];
        ClaimsIdentity identity = new(claims, "Cookies");
        ClaimsPrincipal principal = new(identity);

        await HttpContext.SignInAsync("Cookies", principal);
        return RedirectToPage("/Usuarios/Index");
    }
}
